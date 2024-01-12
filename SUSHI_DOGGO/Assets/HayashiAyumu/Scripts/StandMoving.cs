using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody standRB;

    //  �e�s��������Ă��邩�̔���t���O
    public bool isJumping = false;
    private bool isCurving = false;
    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE�̍��v6���[��
    public int _laneNamber;

    //  ���V��O�ǉ���
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
        //  X��float���ړ���
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

        //  �ǉ���
        dogController = new DogController();
        dogController.Enable();

        //  �R���g���[���[�ڑ��U�蕪��
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

        //  �u�J�[�u���v�Ȃ�A
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  �R���g���[���[�ڑ����̃W�����v
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
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void PlayerMove()
    {
        
        //  �u�ړ����v�łȂ��A
        if (!isMoving)
        {
            //  �u���͒��v�łȂ����
            if (isKeyUp)
            {
                //  InputSystem �� value ��ǂݍ���
                //  ���� inputVal ��ǂݍ��܂Ȃ��ƁA��x�ړ����Ď��ʁB�Ȃ��B
                //var inputVal = dogController.Player.Move.ReadValue<Vector2>();
                //inputVal.y = 0;
                float inputX = 0;
                if (_connectGamepad != null)
                {
                    inputX = _connectGamepad.leftStick.x.ReadValue();
                }
                //  ���̓��͂������
                if (inputX != 0)
                {
                    //  �u���͒��v��
                    isKeyUp = false;
                    //  12/15/��Ƃ̑����͂�������I�I�I
                    //  ���݂� position ����A���ꂼ��ɉ������ړ��������Z
                    _playerGoToPos = _playerTransform.position;
                    

                    //  �E�ړ�
                    if (inputX > 0 && canRightMove)
                    {
                        _playerGoToPos += _addVector[MoveType.Right];
                        _laneNamber++;
                        _laneNamber = Mathf.Min(_laneNamber, 5);
                    }
                    //  ���ړ�
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
                //  InputSystem �� value ��ǂݍ���
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
            var movingPos = _playerTransform.position;
            movingPos.x = Mathf.Lerp(_playerTransform.position.x, _playerGoToPos.x, actionTimer);
            _playerTransform.position = movingPos;
            yield return null;
        }
        //  �u�ړ����v�� false ��
        isMoving = false;
    }
}
