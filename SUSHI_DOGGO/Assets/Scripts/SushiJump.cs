using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class SushiJump : MonoBehaviour
{
    public float jumpPower;
    private Rigidbody rb;
    public bool isJumping = false;

    [SerializeField]
    private DogMoving _dogMoving;

    [SerializeField]
    private JumpJudgement _jumpJudgement;

    [SerializeField]
    private BoxCollider _boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    async void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping)
        {
            _dogMoving.DogJumpMotion();
            rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            isJumping = false;

            _jumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(3));

            _jumpJudgement._jumpCoolTime = true;


            Debug.Log("testttttttttttttttttttt");
        }
    }

    // ÅöÅöí«â¡
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SushiinuSalmonStand"))
        {
           // isJumping = false;
            Debug.Log("false");
        }
    }
}
