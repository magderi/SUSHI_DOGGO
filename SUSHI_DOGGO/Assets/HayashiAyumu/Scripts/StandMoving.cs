using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StandMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody standRB;

    [SerializeField]
    private DogMoving dogMoving;

    //  �e�s��������Ă��邩�̔���t���O
    public bool isJumping = false;
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;

    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE��6���[��
    private int _laneNamber;


    //  �ړ��̐����Ɏg��position�l����
    private float _dogPosX;
    private float _dogGoToPosX;
    private float _fGoX = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        standRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();

        //  �J�n���̎��i���� position.x ���擾
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;

        //  ���ɁA������3�Ԗڂɒu���Ă���z��
        _laneNamber = 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        dogMoving.isJumping = isJumping;

        //  �u�J�[�u���v�Ȃ�A
        if (isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && dogMoving.isJumping == false)
        {
            //  �u�W�����v���v�ɐݒ�
            dogMoving.isJumping = true;
        }
    }

    public void MoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //  X���W���E��10�����ړ�
            _dogGoToPosX += _fGoX;
            //  �u�E�Ɉړ����v�ɐݒ�
            isRightMoving = true;
            //  ���[���i���o�[���C���N�������g
            _laneNamber++;
        }
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            //  X���W������10�����ړ�
            _dogGoToPosX -= _fGoX;
            //  �u���Ɉړ����v�ɐݒ�
            isLeftMoving = true;
            //  ���[���i���o�[���f�N�������g
            _laneNamber--;
        }
    }

    

    /// <summary>
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void PlayerMove()
    {
        //  �u���Ɉړ����v�Ȃ�A
        if (isLeftMoving)
        {
            //  �ݒ肵���ړ���ɓ��B������
            if (_dogPosX <= _dogGoToPosX)
            {
                //  �u���Ɉړ����v���������āA
                isLeftMoving = false;
                //  velocity �� 0 �ɂ��ďI���
                standRB.velocity = Vector3.left * 0;
                return;
            }
            //  ���� position �� x ��ۑ����āA���ɗ͂�������
            _dogPosX = transform.position.x;
            //  AddForce �ŕ��������������邩�A velocity �ŃA�j���`�b�N�ȓ����ɂ��邩
            //dogRB.AddForce(Vector3.left * dogStatus._movePower);
            standRB.velocity = Vector3.left * dogStatus._movePower;
        }
        //  �u�E�Ɉړ����v�Ȃ�A
        if (isRightMoving)
        {
            //  �ݒ肵���ړ���ɓ��B������
            if (_dogPosX >= _dogGoToPosX)
            {
                //  �u�E�Ɉړ����v���������āA
                isRightMoving = false;
                //  velocity �� 0 �ɂ��ďI���;
                standRB.velocity = Vector3.right * 0;
                return;
            }
            //  ���� position �� x ��ۑ����āA�E�ɗ͂�������
            _dogPosX = transform.position.x;
            //dogRB.AddForce(Vector3.right * dogStatus._movePower);
            standRB.velocity = Vector3.right * dogStatus._movePower;
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
}
