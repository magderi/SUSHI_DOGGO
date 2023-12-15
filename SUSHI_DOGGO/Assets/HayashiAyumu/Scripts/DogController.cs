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
        playerController = new PlayerController();
        playerController.Enable();

        //  開始時の寿司犬の position.x を取得
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;
        _fGoX = 10.0f;

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

    private void Jump(InputAction.CallbackContext context)
    {
        //  「ジャンプ中」に設定
        dogMoving.isJumping = true;
    }


    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
       
        //  InputSystem の value を読み込む
        var inputVal = playerController.Player.Move.ReadValue<Vector2>();
        inputVal.y = 0;
        //  「移動中」でなく、
        if (!isMoving)
        {
            //  「入力中」でなければ
            if (isKeyUp)
            {
                //  横の入力があれば
                if (inputVal.x != 0)
                {
                    //  「移動中」かつ「入力中」に
                    isMoving = true;
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
                else
                {
                    isKeyUp = true;
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
