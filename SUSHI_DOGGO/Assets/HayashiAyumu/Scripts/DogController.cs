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

    //  �e�s��������Ă��邩�̔���t���O
    public bool isJumping = false;
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;
    private bool isMoving = false;
    private bool isKeyUp = true;

    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE��6���[��
    private int _laneNamber;


    //  �ړ��̐����Ɏg��position�l����
    private float _dogPosX;
    private float _dogGoToPosX;
    private float _fGoX;

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
        playerController = new PlayerController();
        playerController.Enable();

        //  �J�n���̎��i���� position.x ���擾
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;
        _fGoX = 10.0f;

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

    private void Jump(InputAction.CallbackContext context)
    {
        //  �u�W�����v���v�ɐݒ�
        dogMoving.isJumping = true;
    }


    /// <summary>
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void PlayerMove()
    {
       
        //  InputSystem �� value ��ǂݍ���
        var inputVal = playerController.Player.Move.ReadValue<Vector2>();
        inputVal.y = 0;
        //  �u�ړ����v�łȂ��A
        if (!isMoving)
        {
            //  �u���͒��v�łȂ����
            if (isKeyUp)
            {
                //  ���̓��͂������
                if (inputVal.x != 0)
                {
                    //  �u�ړ����v���u���͒��v��
                    isMoving = true;
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
                else
                {
                    isKeyUp = true;
                }
            }
        }
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
