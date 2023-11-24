using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody dogRB;
    [SerializeField]
    private GameObject stand;
    [SerializeField]
    private StandMoving standMoving;

    //  �e�s��������Ă��邩�̔���t���O
    public  bool isJumping = false;
    private  bool isJumpCooling = false;
    private bool isCurving = false;

    //  �W�����v�Ɏg���N�[���^�C��
    [SerializeField]
    private float _jumpCoolTime = 1.0f;
    private float _jumpedTimer = 0f;

    //  �ړ��̐����Ɏg��position�l����
    private float _dogPosX;
    private float _dogGoToPosX;

    Vector3 standVec;
    Vector3 dogVec;
    float standX;
    float standZ;
    // Start is called before the first frame update
    void Start()
    {
        dogRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();


        standVec = stand.transform.position;
        standX = standVec.x;
        standZ = standVec.z;

        dogVec = this.transform.position;
        dogVec.x = standX;
        dogVec.z = standZ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        JumpCoolTime();
        DogMove();
          
        //  �u�J�[�u���v�Ȃ�A
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    /// <summary>
    /// ���i�����A���Ŕ�ׂȂ��悤��
    /// </summary>
    private void JumpCoolTime()
    {
        //  �N�[���^�C�����߂�����A
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  �u�W�����v���v������
            isJumpCooling = false;
            _jumpedTimer = 0f;
            standMoving.isJumping = isJumpCooling;
        }

        //  �W�����v���Ă���̕b�����v��
        if (isJumping && isJumpCooling == false)
        {
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            Debug.Log("�W�����v�Ȃ�");
            isJumping = false;
            isJumpCooling = true;
        }
        if(isJumpCooling)
        {
            _jumpedTimer += Time.deltaTime;
            return;
        }
    }

    /// <summary>
    /// ���i���̍��E�̈ړ�����
    /// </summary>
    private void DogMove()
    {
        standVec = stand.transform.position;
        standX = standVec.x;
        standZ = standVec.z;

        this.transform.position = new Vector3 (standX, this.transform.position.y, standZ);
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
