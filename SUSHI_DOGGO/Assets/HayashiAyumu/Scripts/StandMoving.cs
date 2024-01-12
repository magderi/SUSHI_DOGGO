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
    private bool isCurving = false;
    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の合計6レーン
    public int _laneNamber;

    //  試遊会直前追加分
    private bool isMoving = false;
    private bool isKeyUp = true;
    public bool canRightMove = true;
    public bool canLeftMove = true;

    [SerializeField]
    private DogMoving dogMoving;

    private DogController dogController;
    [SerializeField]
    private Transform _playerTransform;

    private Vector3 _playerGoToPos;

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

    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        //  追加分
        dogController = new DogController();
        dogController.Enable();

        //  コントローラー接続振り分け
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        if(_connectGamepadNum == 0)
        {
            _laneNamber = 1;
        }
        else if(_connectGamepadNum == 1)
        {
            _laneNamber = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();

        dogMoving.isJumping = isJumping;

        //  「カーブ中」なら、
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  コントローラー接続時のジャンプ
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

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        
        //  「移動中」でなく、
        if (!isMoving)
        {
            //  「入力中」でなければ
            if (isKeyUp)
            {
                //  InputSystem の value を読み込む
                //  逐一 inputVal を読み込まないと、一度移動して死ぬ。なぜ。
                //var inputVal = dogController.Player.Move.ReadValue<Vector2>();
                //inputVal.y = 0;
                float inputX = 0;
                if (_connectGamepad != null)
                {
                    inputX = _connectGamepad.leftStick.x.ReadValue();
                }
                //  横の入力があれば
                if (inputX != 0)
                {
                    //  「入力中」に
                    isKeyUp = false;
                    //  12/15/作業の続きはここから！！！
                    //  現在の position から、それぞれに応じた移動幅を加算
                    _playerGoToPos = _playerTransform.position;
                    

                    //  右移動
                    if (inputX > 0 && canRightMove)
                    {
                        _playerGoToPos += _addVector[MoveType.Right];
                        _laneNamber++;
                        _laneNamber = Mathf.Min(_laneNamber, 5);
                    }
                    //  左移動
                    else if (inputX < 0 && canLeftMove)
                    {
                        _playerGoToPos += _addVector[MoveType.Left];
                        _laneNamber--;
                        _laneNamber = Mathf.Max(0, _laneNamber);
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
                    isKeyUp = true;
                    canLeftMove = true;
                    canRightMove = true;
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

    private IEnumerator MoveCor()
    {
        //  「移動中」に
        isMoving = true;
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
        isMoving = false;
    }
}
