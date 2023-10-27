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
        //  ジャンプしたら3.0秒間、操作を受け付けない

        PlayerJump();
        PlayerMove();

    }

    private void PlayerJump()
    {
        //  CoolTime が終わったら
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  ジャンプ中を無効化
            isjumping = false;
            _jumpedTimer = 0f;
        }
        if (isjumping)
        {
            Debug.Log("クールタイム");
            _jumpedTimer += Time.deltaTime;
            return;
        }
        //  一先ずスペースでジャンプ
        //  後々 InputSystem に切り替え
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
