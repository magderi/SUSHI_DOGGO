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
    private bool _isCurving = false;
    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の合計6レーン
    public int laneNamber;

    //  試遊会直前追加分
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
        { MoveType.Left, new Vector3(-1.1f, 0, 0) },
        { MoveType.Right, new Vector3(1.1f, 0, 0) },
    };

    [SerializeField]
    public int _connectGamepadNum;
    public Gamepad _connectGamepad;

    [SerializeField]
    private SushiJump sushiJump;

    /*
    //  デバック時に使用する変数たち(バグ発生中)
    private InputAction tunaJump = null;
    private InputAction salmonJump = null;
    bool salmonJumping = false;
    */

    /// <summary>
    /// Scene 開始処理
    /// </summary>
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        _ISPlayerMove = new ISPlayerMove();
        _ISPlayerMove.Enable();

        //  コントローラー接続振り分け
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        //  マグロ
        if(_connectGamepadNum == 0)
        {
            laneNamber = 1;
        }
        //  サーモン
        else if(_connectGamepadNum == 1)
        {
            laneNamber = 5;
        }

        /*
        //  デバック用初期設定
        //  ここでバグが発生していて、ISPlayerMoveそのものがnullになっている。
        tunaJump = ISPlayerMove.DebugTuna.Jump;
        salmonJump = ISPlayerMove.DebugSalmon.Jump;
        */

        _stickUIMiniPos = _stickUIMini.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();

        _dogMoving.isJumping = isJumping;

        //  「カーブ中」なら、
        if (_isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  コントローラー接続時のジャンプ
            if (_connectGamepad != null)
            {
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
            /*
            //  デバック用(キーボード操作)
            else
            {
                bool tunaJumping = tunaJump.WasPressedThisFrame();
                if (tunaJumping)
                {
                    sushiJump.isMaguroJump = true;
                }
                else
                {
                    sushiJump.isMaguroJump = false;
                }

                salmonJumping = salmonJump.WasPressedThisFrame();
                if (salmonJumping)
                {
                    sushiJump.isSalmonJump = true;
                }
                else
                {
                    sushiJump.isSalmonJump = false;
                }
            }*/
        }
    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        
        //  「移動中」でなく、「ジャンプ中」でなければ
        if (!_isMoving && !isJumping)
        {
            //  「入力中」でなければ
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
                    //  「入力中」に
                    _isKeyUp = false;
                    //  現在の position から、それぞれに応じた移動幅を加算
                    _playerGoToPos = _playerTransform.position;
                    

                    //  右移動
                    if (inputX > 0)
                    {
                        //  スティック入力のUI操作
                        _stickUIMiniPos = new Vector2(20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canRightMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Right];
                            laneNamber++;
                            laneNamber = Mathf.Min(laneNamber, 5);
                        }}
                    //  左移動
                    else if (inputX < 0)
                    {
                        //  スティック入力のUI操作
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
                //  InputSystem の value を読み込む
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

    /// <summary>
    /// カーブ時の処理(未完成)
    /// </summary>
    /// <param name="MaxMoveLimit"></param>
    private void CurveMoveLimit(float MaxMoveLimit)
    {
        //  移動速度に制限をつけて滑らかに動かそうとしている...はず？
        float _currentMoveSpeed = standRB.velocity.z;
        if (_currentMoveSpeed > MaxMoveLimit)
        {
            _currentMoveSpeed /= MaxMoveLimit;
            standRB.velocity = new Vector3(
                standRB.velocity.x,
                standRB.velocity.y,
                _currentMoveSpeed);
        }
    }

    /// <summary>
    /// 左右の滑らかな移動コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCor()
    {
        //  「移動中」に
        _isMoving = true;
        //  _moveTimer 秒経ったら終わる繰り返し処理
        //  1fと書いてはいるが、1秒で終わるわけではない。
        float actionTimer = 0f;
        while (actionTimer < 1f)
        {
            actionTimer += Time.deltaTime / dogStatus._moveTimer;
            actionTimer = Mathf.Min(actionTimer, 1f);
            //  線型補間を使って道中の移動を自然に
            var movingPos = _playerTransform.position;
            movingPos.x = Mathf.Lerp(_playerTransform.position.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  「移動中」を false に
        _isMoving = false;
    }
}
