using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody standRB;

    public bool isJumping = false;

    //  寿司犬のいるレーンを識別するためのint
    //  0~5の6レーンで、サーモンが1、マグロが4
    public int laneNamber;

    private bool _isMoving = false;
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
        //  Xの移動幅をfloatで設定
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

        //  接続しているGamepadの番号を取得
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        //  最初に接続した方がサーモン
        if(_connectGamepadNum == 0)
        {
            laneNamber = 1;
        }
        //  後に接続した方がマグロ
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
    }

    //  
    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  Gamepadを接続しているとき限定で入力を受ける
            bool inputPress = false;
            if (_connectGamepad != null)
            {
                inputPress = _connectGamepad.buttonSouth.wasPressedThisFrame;
            }
            bool inputSalmonJump = Input.GetKey(KeyCode.W);
            bool inputMaguroJump = Input.GetKey(KeyCode.UpArrow);

            if(inputPress)
            {
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

            if(inputMaguroJump || inputSalmonJump)
            {
                if(_connectGamepadNum == 0 && inputSalmonJump)
                    sushiJump.isSalmonJump = true;
                if(_connectGamepadNum == 1 && inputMaguroJump)
                    sushiJump.isMaguroJump = true;
            }
            else
            {
                if(_connectGamepadNum == 0 && inputSalmonJump)
                    sushiJump.isSalmonJump = false;
                if(_connectGamepadNum == 1 && inputMaguroJump)
                    sushiJump.isMaguroJump = false;
            }
        }
    }

    /// <summary>
    /// Playerの移動処理
    /// </summary>
    private void PlayerMove()
    {
        
        //  ジャンプ中や移動中の時は移動入力を受けないように
        if (!_isMoving && !isJumping)
        {
            //  コントローラーのスティック入力受付
            float inputX = 0;
            if (_connectGamepad != null)
            {
                //  InputSystem で左スティックの value を読み込む
                inputX = _connectGamepad.leftStick.x.ReadValue();
            }

            //  キーボードでの移動入力用
            if(_connectGamepadNum == 0)
            {
                if(Input.GetKey(KeyCode.A))
                {
                    inputX = -1;
                }
                else if(Input.GetKey(KeyCode.D))
                {
                    inputX = 1;
                }
            }
            else if(_connectGamepadNum == 1)
            {
                if(Input.GetKey(KeyCode.LeftArrow))
                {
                    inputX = -1;
                }
                else if(Input.GetKey(KeyCode.RightArrow))
                {
                    inputX = 1;
                }
            }

            if (inputX != 0)
            {
                //  現在のPlayerのpositionを保存
                _playerGoToPos = _playerTransform.position;
                

                //  右へ入力したら
                if (inputX > 0)
                {
                    //  スティックのUIを右に
                    _stickUIMiniPos = new Vector2(20f, 0);
                    _stickUIMini.transform.localPosition = _stickUIMiniPos;
                    //  右への移動が可能なら
                    if (canRightMove)
                    {
                        _playerGoToPos += _addVector[MoveType.Right];
                        laneNamber++;
                        laneNamber = Mathf.Min(laneNamber, 5);
                    }}
                //  左へ入力したら
                else if (inputX < 0)
                {
                    //  スティックのUIを左に
                    _stickUIMiniPos = new Vector2(-20f, 0);
                    _stickUIMini.transform.localPosition = _stickUIMiniPos;
                    //  左への移動が可能なら
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
            else
            {
                canLeftMove = true;
                canRightMove = true;
                _stickUIMini.transform.localPosition = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// 移動のコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCor()
    {
        _isMoving = true;
        //  DogStatusの_moveTimerの時間で横に移動する 
        float actionTimer = 0f;
        while (actionTimer < 1f)
        {
            actionTimer += Time.deltaTime / dogStatus._moveTimer;
            actionTimer = Mathf.Min(actionTimer, 1f);
            var movingPos = _playerTransform.position;
            movingPos.x = Mathf.Lerp(_playerTransform.position.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        _isMoving = false;
    }
}
