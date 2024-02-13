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

    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE�̍��v6���[��
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
        //  X��float���ړ���
        { MoveType.Left, new Vector3(-1.17f, 0, 0) },
        { MoveType.Right, new Vector3(1.17f, 0, 0) },
    };

    [SerializeField]
    public int _connectGamepadNum;
    public Gamepad _connectGamepad;

    [SerializeField]
    private SushiJump sushiJump;


    /// <summary>
    /// Scene �J�n����
    /// </summary>
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        _ISPlayerMove = new ISPlayerMove();
        _ISPlayerMove.Enable();

        //  �R���g���[���[�ڑ��U�蕪��
        _connectGamepad = Gamepad.all[_connectGamepadNum];

        //  �T�[����
        if(_connectGamepadNum == 0)
        {
            laneNamber = 1;
        }
        //  �}�O��
        else if(_connectGamepadNum == 1)
        {
            laneNamber = 4;
        }

        /*
        //  �f�o�b�N�p�����ݒ�
        //  �����Ńo�O���������Ă��āAISPlayerMove���̂��̂�null�ɂȂ��Ă���B
        tunaJump = ISPlayerMove.DebugTuna.Jump;
        salmonJump = ISPlayerMove.DebugSalmon.Jump;
        */

        _stickUIMiniPos = _stickUIMini.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerJump();

        _dogMoving.isJumping = isJumping;
        /*
        //  �u�J�[�u���v�Ȃ�A
        if (_isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
        */
    }

    public void PlayerJump()
    {
        if(isJumping == false)
        {
            //  �R���g���[���[�ڑ����̃W�����v
            if (_connectGamepad != null)
            {
                //  �L�[�{�[�h�̉��ɂ���{�^��(XBOX�Ȃ�A�{�^��)�����������̔���
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
    }

    /// <summary>
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void PlayerMove()
    {
        
        //  �u�ړ����v�łȂ��A�u�W�����v���v�łȂ����
        if (!_isMoving && !isJumping)
        {
            //  �u���͒��v�łȂ����
            if (_isKeyUp)
            {
                float inputX = 0;
                if (_connectGamepad != null)
                {
                    //  InputSystem �� value ��ǂݍ���
                    inputX = _connectGamepad.leftStick.x.ReadValue();
                }
                //  ���̓��͂������
                if (inputX != 0)
                {
                    //  �u���͒��v��
                    _isKeyUp = false;
                    //  ���݂� position ����A���ꂼ��ɉ������ړ��������Z
                    _playerGoToPos = _playerTransform.position;
                    

                    //  �E�ړ�
                    if (inputX > 0)
                    {
                        //  �X�e�B�b�N���͂�UI����
                        _stickUIMiniPos = new Vector2(20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

                        if (canRightMove)
                        {
                            _playerGoToPos += _addVector[MoveType.Right];
                            laneNamber++;
                            laneNamber = Mathf.Min(laneNamber, 5);
                        }}
                    //  ���ړ�
                    else if (inputX < 0)
                    {
                        //  �X�e�B�b�N���͂�UI����
                        _stickUIMiniPos = new Vector2(-20f, 0);
                        _stickUIMini.transform.localPosition = _stickUIMiniPos;

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
            }
            else
            {
                //  InputSystem �� value ��ǂݍ���
                //var inputVal = dogController.Player.Move.ReadValue<Vector2>();
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

    /*
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
    }*/

    /// <summary>
    /// ���E�̊��炩�Ȉړ��R���[�`��
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCor()
    {
        //  �u�ړ����v��
        _isMoving = true;
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
        _isMoving = false;
    }
}
