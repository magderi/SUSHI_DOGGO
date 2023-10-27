using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    private bool isjumping = false;

    [SerializeField]
    private float _jumpCoolTime = 1.0f;
    private float _jumpedTimer = 0f;

    private DogStatus dogStatus;
    protected Rigidbody dogRB;
    // Start is called before the first frame update
    void Start()
    {
        dogRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        //  �W�����v������3.0�b�ԁA������󂯕t���Ȃ�

        PlayerJump();
        PlayerMove();

    }

    private void PlayerJump()
    {
        //  CoolTime ���I�������
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  �W�����v���𖳌���
            isjumping = false;
            _jumpedTimer = 0f;
        }
        if (isjumping)
        {
            Debug.Log("�N�[���^�C��");
            _jumpedTimer += Time.deltaTime;
            return;
        }
        //  ��悸�X�y�[�X�ŃW�����v
        //  ��X InputSystem �ɐ؂�ւ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            isjumping = true;
        }

    }
    private void PlayerMove()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            dogRB.AddForce(Vector3.left * dogStatus._movePower);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            dogRB.AddForce(Vector3.right * dogStatus._movePower);
        }
    }
}
