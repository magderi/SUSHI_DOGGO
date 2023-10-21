using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float velocity = 5f;
    public float jumpPower;
    public Rigidbody rb;

    private bool isJumping = false;
    private float speed = 30f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpPower;
            // ★追加
            isJumping = true;
            Debug.Log("kkkkkkkkkkkk");            
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.MovePosition(transform.position + new Vector3(-1, 0, 0));
            Debug.Log("aaaa");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.MovePosition(transform.position + new Vector3(1, 0, 0));
        }
    }
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 10)
        {
            //指定したスピードから現在の速度を引いて加速力を求める
            float currentSpeed = speed - rb.velocity.magnitude;
            //調整された加速力で力を加える
            rb.AddForce(new Vector3(0, 0, currentSpeed));
        }

    }

    // ★★追加
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
