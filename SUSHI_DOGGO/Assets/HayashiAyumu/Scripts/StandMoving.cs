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
    private bool isRightMoving = false;
    private bool isLeftMoving = false;

    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の6レーン
    private int _laneNamber;


    //  移動の制限に使うposition値入れ
    private float _dogPosX;
    private float _dogGoToPosX;
    private float _fGoX = 1.1f;

    //  追加分
    [SerializeField]
    private DogMoving dogMoving;

    private DogController dogController;
    [SerializeField]
    private Transform _playerTransform;

    private Vector3 _playerCarrentPos;
    private Vector3 _playerGoToPos;

    private bool isMoving = false;
    private bool isKeyUp = true;

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

    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        //  開始時の寿司犬の position.x を取得
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;

        //  仮に、左から3番目に置いてある想定
        _laneNamber = 2;

        //  追加分
        dogController = new DogController();
        dogController.Enable();

        _playerCarrentPos = _playerTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();

        dogMoving.isJumping = isJumping;

        //  「カーブ中」なら、
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    //public void Jump(InputAction.CallbackContext context)
    public void PlayerJump()
    {
        if(isJumping == false)
        {
            var inputVal = dogController.Player.Jump.triggered;
            if(inputVal)
            {
                dogMoving.isJumping = true;
            }
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
        }
        */

        //  「移動中」でなく、
        if (!isMoving)
        {
            Debug.Log("移動中じゃないよ。");
            //  「入力中」でなければ
            if (isKeyUp)
            {
                Debug.Log("入力中じゃないよ。");

                //  InputSystem の value を読み込む
                //  逐一 inputVal を読み込まないと、一度移動して死ぬ。なぜ。
                var inputVal = dogController.Player.Move.ReadValue<Vector2>();
                inputVal.y = 0;
                //  横の入力があれば
                if (inputVal.x != 0)
                {
                    //  「入力中」に
                    isKeyUp = false;
                    //  12/15/作業の続きはここから！！！
                    //  現在の position から、それぞれに応じた移動幅を加算
                    _playerGoToPos = Vector3.zero;
                    if (inputVal.x > 0)
                        _playerGoToPos = _playerCarrentPos + _addVector[MoveType.Right];
                    else
                        _playerGoToPos = _playerCarrentPos + _addVector[MoveType.Left];
                    StartCoroutine(MoveCor());
                }
            }
            else
            {
                //  InputSystem の value を読み込む
                var inputVal = dogController.Player.Move.ReadValue<Vector2>();
                if (inputVal.x == 0)
                    isKeyUp = true;
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
            var movingPos = _playerCarrentPos;
            movingPos.x = Mathf.Lerp(_playerCarrentPos.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  現在の position を代入
        _playerCarrentPos = _playerTransform.position;
        //  「移動中」を false に
        isMoving = false;
    }
}
