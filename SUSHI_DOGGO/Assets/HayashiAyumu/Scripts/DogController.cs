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
    [SerializeField]
    private Transform _playerTransform;

    private PlayerController moveController;
    private PlayerController jumpController;

    //  各行動を取っているかの判定フラグ
    public bool isJumping = false;
    private bool isCurving = false;
    private bool isMoving = false;
    private bool isKeyUp = true;

    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の6レーン
    private int _laneNamber;


    //  移動の制限に使うposition値入れ
    private float _dogPosX;

    private Vector3 _playerPos;
    private Vector3 _playerGoToPos;
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
        moveController = new PlayerController();
        moveController.Enable();

        _playerPos = _playerTransform.position;

        //  仮に、左から3番目に置いてある想定
        _laneNamber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        dogMoving.isJumping = isJumping;

        PlayerMove();
        //  「カーブ中」なら、(今のところ未実装)
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }


    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
       
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
                var inputVal = moveController.Player.Move.ReadValue<Vector2>();
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
                        _playerGoToPos = _playerPos + _addVector[MoveType.Right];
                    else
                        _playerGoToPos = _playerPos + _addVector[MoveType.Left];
                    StartCoroutine(MoveCor());
                }
            }
            else
            {
                //  InputSystem の value を読み込む
                var inputVal = moveController.Player.Move.ReadValue<Vector2>();
                if (inputVal.x == 0)
                    isKeyUp = true;
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        var inputVal = moveController.Player.Jump.triggered;
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
            var movingPos = _playerPos;
            movingPos.x = Mathf.Lerp(_playerPos.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  現在の position を代入
        _playerPos = _playerTransform.position;
        //  「移動中」を false に
        isMoving = false;
    }
}
