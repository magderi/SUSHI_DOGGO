using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//  台を動かすことで普段の左右移動を管理するスクリプト
public class StandMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody standRB;

    //  ジャンプ中は移動を無効に
    public bool isJumping = false;

    //  寿司犬の現在いるレーンを識別するためのint値
    //  0~5の6通りで、サーモンは１，マグロは４からスタート
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
        //  左右の移動幅
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

        //  接続されたゲームパッドの番号を取得
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        //  先に接続されたならサーモン
        if(_connectGamepadNum == 0)
        {
            laneNamber = 1;
        }
        //  後に接続されたならマグロ
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

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  ゲームパッドが接続されているなら
            if (_connectGamepad != null)
            {
                //  Aボタンが押された際の挙動
                bool inputPress = _connectGamepad.buttonSouth.wasPressedThisFrame;
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
            }
        }
    }

    /// <summary>
    /// 寿司犬たちの左右移動を管理する関数
    /// </summary>
    private void PlayerMove()
    {
        
        //  移動可能な状況なら
        if (!_isMoving && !isJumping)
        {
            //  スティックを傾けていれば
            if (_isKeyUp)
            {
                float inputX = 0;
                if (_connectGamepad != null)
                {
                    //  InputSystemで左スティックのxのvalueを取得 
                    inputX = _connectGamepad.leftStick.x.ReadValue();
                }
                //  左スティックを左右に傾けていれば
                if (inputX != 0)
                {
                    //  操作中に
                    _isKeyUp = false;
                    //  現在の寿司犬のpositionを取得
                    _playerGoToPos = _playerTransform.position;
                    

                    //  右に移動
                    if (inputX > 0)
                    {
                        //  スティックのUIを動かす
                        _stickUIMiniPos = new Vector2(20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canRightMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Right];
                            laneNamber++;
                            laneNamber = Mathf.Min(laneNamber, 5);
                        }}
                    //  左に移動
                    else if (inputX < 0)
                    {
                        //  スティックのUIを動かす
                        _stickUIMiniPos = new Vector2(-20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canLeftMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Left];
                            laneNamber--;
                            laneNamber = Mathf.Max(0, laneNamber);
                        }
                    }
                    StartCoroutine(MoveCor());
                }
            }
            else
            {
                //  移動のUIを元に戻す
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
    /// 左右移動をなめらかにするためのコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCor()
    {
        //  移動中に
        _isMoving = true;
        float actionTimer = 0f;
        while (actionTimer < 1f)
        {
            //  _moveTimer分の時間をかけて左右に移動する
            actionTimer += Time.deltaTime / dogStatus._moveTimer;
            actionTimer = Mathf.Min(actionTimer, 1f);
            var movingPos = _playerTransform.position;
            movingPos.x = Mathf.Lerp(_playerTransform.position.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  移動中を偽に
        _isMoving = false;
    }
}
