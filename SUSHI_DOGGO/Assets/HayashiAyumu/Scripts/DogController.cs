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

    //  �e�s��������Ă��邩�̔���t���O
    public bool isJumping = false;
    private bool isCurving = false;
    private bool isMoving = false;
    private bool isKeyUp = true;

    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE��6���[��
    private int _laneNamber;


    //  �ړ��̐����Ɏg��position�l����
    private float _dogPosX;

    private Vector3 _playerPos;
    private Vector3 _playerGoToPos;
    [SerializeField]
    private float _playerGoToPosX = 1.2f;

    //  �ړ��̎�ޓ���
    private enum MoveType
    {
        None,
        Left,
        Right,
    }
    private Dictionary<MoveType, Vector3> _addVector = new Dictionary<MoveType, Vector3>()
    {
        //  X��float���ړ���
        { MoveType.Left, new Vector3(-3.1f, 0, 0) },
        { MoveType.Right, new Vector3(3.1f, 0, 0) },
    };

    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
        //  InputSystem ��ǂݍ���ŁAEnable �ŗL����
        moveController = new PlayerController();
        moveController.Enable();

        _playerPos = _playerTransform.position;

        //  ���ɁA������3�Ԗڂɒu���Ă���z��
        _laneNamber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        dogMoving.isJumping = isJumping;

        PlayerMove();
        //  �u�J�[�u���v�Ȃ�A(���̂Ƃ��떢����)
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }


    /// <summary>
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void PlayerMove()
    {
       
        //  �u�ړ����v�łȂ��A
        if (!isMoving)
        {
            Debug.Log("�ړ�������Ȃ���B");
            //  �u���͒��v�łȂ����
            if (isKeyUp)
            {
                Debug.Log("���͒�����Ȃ���B");

                //  InputSystem �� value ��ǂݍ���
                //  ���� inputVal ��ǂݍ��܂Ȃ��ƁA��x�ړ����Ď��ʁB�Ȃ��B
                var inputVal = moveController.Player.Move.ReadValue<Vector2>();
                inputVal.y = 0;
                //  ���̓��͂������
                if (inputVal.x != 0)
                {
                    //  �u���͒��v��
                    isKeyUp = false;
                    //  12/15/��Ƃ̑����͂�������I�I�I
                    //  ���݂� position ����A���ꂼ��ɉ������ړ��������Z
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
                //  InputSystem �� value ��ǂݍ���
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
    /// �J�[�u���̏���(������)
    /// </summary>
    /// <param name="MaxMoveLimit"></param>
    private void CurveMoveLimit(float MaxMoveLimit)
    {
        //  �ړ����x�ɐ��������Ċ��炩�ɓ��������Ƃ��Ă���...�͂��H
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
        //  �u�ړ����v��
        isMoving = true;
        //  _moveTimer �b�o������I���J��Ԃ�����
        //  1f�Ə����Ă͂��邪�A1�b�ŏI���킯�ł͂Ȃ��B
        float actionTimer = 0f;
        while (actionTimer < 1f)
        {
            actionTimer += Time.deltaTime / dogStatus._moveTimer;
            actionTimer = Mathf.Min(actionTimer, 1f);
            //  ���^��Ԃ��g���ē����̈ړ������R��
            var movingPos = _playerPos;
            movingPos.x = Mathf.Lerp(_playerPos.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  ���݂� position ����
        _playerPos = _playerTransform.position;
        //  �u�ړ����v�� false ��
        isMoving = false;
    }
}
