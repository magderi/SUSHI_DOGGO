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
            // Åöí«â¡
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
     

    }

    // ÅöÅöí«â¡
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }
}
