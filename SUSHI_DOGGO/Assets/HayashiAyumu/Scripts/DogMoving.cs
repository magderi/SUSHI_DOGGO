using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody dogRB;

    //  �e�s��������Ă��邩�̔���t���O
    public  bool isJumping = false;
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;

    //  �����郌�[���𔻒肷�邽�߂�int�l
    //  0����ԍ��A5����ԉE��6���[��
    private int _laneNamber;

    //  �W�����v�Ɏg���N�[���^�C��
    [SerializeField]
    private float _jumpCoolTime = 1.0f;
    private float _jumpedTimer = 0f;

    //  �ړ��̐����Ɏg��position�l����
    private float _dogPosX;
    private float _dogGoToPosX;

    // Start is called before the first frame update
    void Start()
    {
        dogRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
        
        //  �J�n���̎��i���� position.x ���擾
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;

        //  ���ɁA������3�Ԗڂɒu���Ă���z��
        _laneNamber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        JumpCoolTime();
        PlayerMove();
          
        //  �u�J�[�u���v�Ȃ�A
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isJumping == false)
        {
            //  status�Őݒ肵�Ă���͂̕��A��ɔ��
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            //  �u�W�����v���v�ɐݒ�
            isJumping = true;
        }
    }
    
    public void MoveRight(InputAction.CallbackContext context)
    {   
        if (context.phase == InputActionPhase.Performed)
        {
            //  X���W���E��10�����ړ�
            _dogGoToPosX += 10.0f;
            //  �u�E�Ɉړ����v�ɐݒ�
            isRightMoving = true;
            //  ���[���i���o�[���C���N�������g
            _laneNamber++;
        }
        else if (context.phase == InputActionPhase.Canceled)
            //  ���͂𗣂�����u�E�Ɉړ����v������
            isRightMoving = false;
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {    
        if (context.phase == InputActionPhase.Performed)
        {
            //  X���W������10�����ړ�
            _dogGoToPosX -= 10.0f;
            //  �u���Ɉړ����v�ɐݒ�
            isLeftMoving = true;
            //  ���[���i���o�[���f�N�������g
            _laneNamber--;
        }
        else if (context.phase == InputActionPhase.Canceled)
            //  ���͂𗣂�����u���Ɉړ����v������
            isLeftMoving = false;
    }

    /// <summary>
    /// ���i�����A���Ŕ�ׂȂ��悤��
    /// </summary>
    private void JumpCoolTime()
    {
        //  �N�[���^�C�����Ȃ�A
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  �u�W�����v���v������
            isJumping = false;
            _jumpedTimer = 0f;
        }

        //  �W�����v���Ă���̕b�����v��
        if (isJumping)
        {
            _jumpedTimer += Time.deltaTime;
            return;
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
            if(_dogPosX <= _dogGoToPosX)
            {
                //  velocity �� 0 �ɂ��ďI���
                dogRB.velocity = Vector3.left * 0;
                return;
            }
            //  ���� position �� x ��ۑ����āA���ɗ͂�������
            _dogPosX = transform.position.x;
            //  AddForce �ŕ��������������邩�A velocity �ŃA�j���`�b�N�ȓ����ɂ��邩
            //dogRB.AddForce(Vector3.left * dogStatus._movePower);
            dogRB.velocity = Vector3.left * dogStatus._jumpPower;
        }
        //  �u�E�Ɉړ����v�Ȃ�A
        if (isRightMoving)
        {
            //  �ݒ肵���ړ���ɓ��B������
            if (_dogPosX >= _dogGoToPosX)
            {
                //  velocity �� 0 �ɂ��ďI���;
                dogRB.velocity = Vector3.right * 0;
                return;
            }
            //  ���� position �� x ��ۑ����āA�E�ɗ͂�������
            _dogPosX = transform.position.x;
            //dogRB.AddForce(Vector3.right * dogStatus._movePower);
            dogRB.velocity = Vector3.right * dogStatus._movePower;
        }
    }

    /// <summary>
    /// �J�[�u���̏���(������)
    /// </summary>
    /// <param name="MaxMoveLimit"></param>
    private void CurveMoveLimit(float MaxMoveLimit)
    {
        //  �ړ����x�ɐ��������Ċ��炩�ɓ��������Ƃ��Ă���...�͂��H
        float _currentMoveSpeed = dogRB.velocity.z;
        if(_currentMoveSpeed > MaxMoveLimit)
        {
            _currentMoveSpeed /= MaxMoveLimit;
            dogRB.velocity = new Vector3(
                dogRB.velocity.x,
                dogRB.velocity.y, 
                _currentMoveSpeed);
        }
    }
}
