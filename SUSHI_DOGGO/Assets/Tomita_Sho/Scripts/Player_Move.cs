using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public float velocity = 5f;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //à⁄ìÆë¨ìxÇíºê⁄ïœçXÇ∑ÇÈ
        rb.velocity = new Vector3(0, 0, velocity);

    }
}
