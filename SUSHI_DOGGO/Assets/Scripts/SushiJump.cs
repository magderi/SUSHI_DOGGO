using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiJump : MonoBehaviour
{
    public float jumpPower;
    private Rigidbody rb;
    public bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity = Vector3.up * jumpPower;
            isJumping = true;
        }
    }

    // ÅöÅöí«â¡
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SushiinuSalmonStand"))
        {
            isJumping = false;
            Debug.Log("false");
        }
    }
}
