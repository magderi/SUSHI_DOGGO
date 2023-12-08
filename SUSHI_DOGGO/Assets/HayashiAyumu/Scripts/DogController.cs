using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogController : MonoBehaviour
{
    public Rigidbody standRB;

    [SerializeField]
    private DogMoving dogMoving;
    [SerializeField]
    private DogStatus dogStatus;
    private PlayerController playerController;
    [SerializeField]
    private Transform _playerTransform;

    //  各行動を取っているかの判定フラグ
    public bool isJumping = false;
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;
    private bool isMoving = false;
    private bool isKeyUp = true;

    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の6レーン
    private int _laneNamber;


    //  移動の制限に使うposition値入れ
    private float _dogPosX;
    private float _dogGoToPosX;
    private float _fGoX;

    private Vector3 _playerPos;
    [SerializeField]
    private float _playerGoToPosX = 1.2f;

    //  移動の種類入れ
    private enum MoveType
    {
        None,
        Left,
        Right,
    }
    private Dictionary<MoveType, Vector3> _addVector = new Dictionary<MoveType, Vector3>()
    {
        //  Xのfloatが移動幅
        { MoveType.Left, new Vector3(-3.1f, 0, 0) },
        { MoveType.Right, new Vector3(3.1f, 0, 0) },
    };

    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
        //  InputSystem を読み込んで、Enable で有効化
        playerController = new PlayerController();
        playerController.Enable();

        //  開始時の寿司犬の position.x を取得
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;
        _fGoX = 10.0f;

        //  仮に、左から3番目に置いてある想定
        _laneNamber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        dogMoving.isJumping = isJumping;

        //  移動中でなく、
        if (isMoving == false)
        {
            //  キー入力がされていなければ、
            if (isKeyUp)
            {

            }
        }

        //  「カーブ中」なら、(今のところ未実装)
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && dogMoving.isJumping == false)
        {
            //  「ジャンプ中」に設定
            dogMoving.isJumping = true;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //  X座標を右に10だけ移動
            _dogGoToPosX += _fGoX;
            //  「右に移動中」に設定
            isRightMoving = true;
            //  レーンナンバーをインクリメント
            _laneNamber++;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //  X座標を左に10だけ移動
            _dogGoToPosX -= _fGoX;
            //  「左に移動中」に設定
            isLeftMoving = true;
            //  レーンナンバーをデクリメント
            _laneNamber--;
        }
    }



    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        /*
        //  「左に移動中」なら、
        if (isLeftMoving)
        {
            //  設定した移動先に到達したら
            if (_dogPosX <= _dogGoToPosX)
            {
                //  「左に移動中」を解除して、
                isLeftMoving = false;
                //  velocity を 0 にして終わる
                standRB.velocity = Vector3.left * 0;
                return;
            }
            //  今の position の x を保存して、左に力を加える
            _dogPosX = transform.position.x;
            //  AddForce で物理挙動をさせるか、 velocity でアニメチックな動きにするか
            //dogRB.AddForce(Vector3.left * dogStatus._movePower);
            standRB.velocity = Vector3.left * dogStatus._movePower;
        }
        //  「右に移動中」なら、
        if (isRightMoving)
        {
            //  設定した移動先に到達したら
            if (_dogPosX >= _dogGoToPosX)
            {
                //  「右に移動中」を解除して、
                isRightMoving = false;
                //  velocity を 0 にして終わる;
                standRB.velocity = Vector3.right * 0;
                return;
            }
            //  今の position の x を保存して、右に力を加える
            _dogPosX = transform.position.x;
            //dogRB.AddForce(Vector3.right * dogStatus._movePower);
            standRB.velocity = Vector3.right * dogStatus._movePower;
        }*/

        //  InputSystem の value を読み込む
        var inputVal = playerController.Player.Move.ReadValue<Vector2>();
        inputVal.y = 0;
        //  横の入力があれば
        if (inputVal.x != 0)
        {
            isMoving = true;
            isKeyUp = false;
            //  作業の続きはここから！！！

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
}
