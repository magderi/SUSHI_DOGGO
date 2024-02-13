using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody standRB;

    //  各行動を取っているかの判定フラグ
    public bool isJumping = false;

    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の合計6レーン
    public int laneNamber;

    private bool _isMoving = false;
    private bool _isKeyUp = true;
    public bool canRightMove = true;
    public bool canLeftMove = true;

    [SerializeField]
    private DogMoving _dogMoving;

    private ISPlayerMove _ISPlayerMove;
    [SerializeField]
    private Transform _playerTransform;
    private Vector3 _playerGoToPos;


    [SerializeField]
    private Transform _stickUIMini;
    private Vector2 _stickUIMiniPos;


    private enum MoveType
    {
        None,
        Left,
        Right,
    }

    private Dictionary<MoveType, Vector3> _addVector = new Dictionary<MoveType, Vector3>()
    {
        //  Xのfloatが移動幅
        { MoveType.Left, new Vector3(-1.17f, 0, 0) },
        { MoveType.Right, new Vector3(1.17f, 0, 0) },
    };

    [SerializeField]
    public int _connectGamepadNum;
    public Gamepad _connectGamepad;

    [SerializeField]
    private SushiJump sushiJump;


    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        _ISPlayerMove = new ISPlayerMove();
        _ISPlayerMove.Enable();

        //  コントローラー接続振り分け
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        //  サーモン
        if(_connectGamepadNum == 0)
        {
            laneNamber = 1;
        }
        //  マグロ
        else if(_connectGamepadNum == 1)
        {
            laneNamber = 4;
        }

        _stickUIMiniPos = _stickUIMini.position;
    }


    void Update()
    {
        PlayerMove();
        PlayerJump();

        _dogMoving.isJumping = isJumping;
        /*
        //  「カーブ中」なら、
        if (_isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
        */
    }

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  コントローラー接続時のジャンプ
            if (_connectGamepad != null)
            {
                //  コントローラーの下に配置されているボタン(XBOXならAボタン)を押したかの判定用
                bool inputPress = _connectGamepad.buttonSouth.wasPressedThisFrame;
                if(inputPress)
                {
                    //dogMoving.isJumping = true;
                    
                    if(_connectGamepadNum == 0)
                        sushiJump.isSalmonJump = true;
                    if(_connectGamepadNum == 1)
                        sushiJump.isMaguroJump = true;
                }
                else
                {
                    if (_connectGamepadNum == 0)
                        sushiJump.isSalmonJump = false;
                    if (_connectGamepadNum == 1)
                        sushiJump.isMaguroJump = false;
                }
            }
        }
    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        
        //  「移動中」でなく、なおかつ「ジャンプ中」でなければ
        if (!_isMoving && !isJumping)
        {
            //  「キー入力中」でなければ
            if (_isKeyUp)
            {
                float inputX = 0;
                if (_connectGamepad != null)
                {
                    //  InputSystem の value を読み込む
                    inputX = _connectGamepad.leftStick.x.ReadValue();
                }
                //  横の入力があれば
                if (inputX != 0)
                {
                    //  「入力中」にする
                    _isKeyUp = false;
                    //  現在の position から、それぞれに応じた移動幅を加算
                    _playerGoToPos = _playerTransform.position;
                    

                    //  ?ｿｽE?ｿｽﾚ難ｿｽ
                    if (inputX > 0)
                    {
                        //  ?ｿｽX?ｿｽe?ｿｽB?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽﾍゑｿｽUI?ｿｽ?ｿｽ?ｿｽ?ｿｽ
                        _stickUIMiniPos = new Vector2(20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canRightMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Right];
                            laneNamber++;
                            laneNamber = Mathf.Min(laneNamber, 5);
                        }}
                    //  ?ｿｽ?ｿｽ?ｿｽﾚ難ｿｽ
                    else if (inputX < 0)
                    {
                        //  ?ｿｽX?ｿｽe?ｿｽB?ｿｽb?ｿｽN?ｿｽ?ｿｽ?ｿｽﾍゑｿｽUI?ｿｽ?ｿｽ?ｿｽ?ｿｽ
                        _stickUIMiniPos = new Vector2(-20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canLeftMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Left];
                            laneNamber--;
                            laneNamber = Mathf.Max(0, laneNamber);
                        }
                    }
                    //if(canRightMove && canLeftMove)
                    StartCoroutine(MoveCor());
                }
            }
            else
            {
                //  InputSystem ?ｿｽ?ｿｽ value ?ｿｽ?ｿｽﾇみ搾ｿｽ?ｿｽ?ｿｽ
                //var inputVal = dogController.Player.Move.ReadValue<Vector2>();
                float inputX = _connectGamepad.leftStick.x.ReadValue();
                if (inputX == 0)
                {
                    _isKeyUp = true;
                    canLeftMove = true;
                    canRightMove = true;

                    _stickUIMini.transform.localPosition = Vector3.zero;
                }
            }
        }
    }

    /*
    /// <summary>
    /// ?ｿｽJ?ｿｽ[?ｿｽu?ｿｽ?ｿｽ?ｿｽﾌ擾ｿｽ?ｿｽ?ｿｽ(?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ)
    /// </summary>
    /// <param name="MaxMoveLimit"></param>
    private void CurveMoveLimit(float MaxMoveLimit)
    {
        //  ?ｿｽﾚ難ｿｽ?ｿｽ?ｿｽ?ｿｽx?ｿｽﾉ撰ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾂゑｿｽ?ｿｽﾄ奇ｿｽ?ｿｽ轤ｩ?ｿｽﾉ難ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽﾆゑｿｽ?ｿｽﾄゑｿｽ?ｿｽ?ｿｽ...?ｿｽﾍゑｿｽ?ｿｽH
        float _currentMoveSpeed = standRB.velocity.z;
        if (_currentMoveSpeed > MaxMoveLimit)
        {
            _currentMoveSpeed /= MaxMoveLimit;
            standRB.velocity = new Vector3(
                standRB.velocity.x,
                standRB.velocity.y,
                _currentMoveSpeed);
        }
    }*/

    /// <summary>
    /// ?ｿｽ?ｿｽ?ｿｽE?ｿｽﾌ奇ｿｽ?ｿｽ轤ｩ?ｿｽﾈ移難ｿｽ?ｿｽR?ｿｽ?ｿｽ?ｿｽ[?ｿｽ`?ｿｽ?ｿｽ
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCor()
    {
        //  ?ｿｽu?ｿｽﾚ難ｿｽ?ｿｽ?ｿｽ?ｿｽv?ｿｽ?ｿｽ
        _isMoving = true;
        //  _moveTimer ?ｿｽb?ｿｽo?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽI?ｿｽ?ｿｽ?ｿｽJ?ｿｽ?ｿｽﾔゑｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ
        //  1f?ｿｽﾆ擾ｿｽ?ｿｽ?ｿｽ?ｿｽﾄはゑｿｽ?ｿｽ驍ｪ?ｿｽA1?ｿｽb?ｿｽﾅ終?ｿｽ?ｿｽ?ｿｽ墲ｯ?ｿｽﾅはなゑｿｽ?ｿｽB
        float actionTimer = 0f;
        while (actionTimer < 1f)
        {
            actionTimer += Time.deltaTime / dogStatus._moveTimer;
            actionTimer = Mathf.Min(actionTimer, 1f);
            //  ?ｿｽ?ｿｽ?ｿｽ^?ｿｽ?ｿｽﾔゑｿｽ?ｿｽg?ｿｽ?ｿｽ?ｿｽﾄ難ｿｽ?ｿｽ?ｿｽ?ｿｽﾌ移難ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽ?ｿｽR?ｿｽ?ｿｽ
            var movingPos = _playerTransform.position;
            movingPos.x = Mathf.Lerp(_playerTransform.position.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  ?ｿｽu?ｿｽﾚ難ｿｽ?ｿｽ?ｿｽ?ｿｽv?ｿｽ?ｿｽ false ?ｿｽ?ｿｽ
        _isMoving = false;
    }
}
