using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class SushiJump : MonoBehaviour
{
    public float jumpPower;
    private Rigidbody rb;
    public bool isSalmonJumping = false;
    public bool isMaguroJumping = false;
    [SerializeField]
    private DogMoving _dogMoving;

    [SerializeField]
    private SalmonJumpJudgement _salmonJumpJudgement;

    [SerializeField]
    private MaguroJumpJudgement _maguroJumpJudgement;

    [SerializeField]
    private BoxCollider _boxCollider;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    async void Update()
    {
        // サーモンジャンプ
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSalmonJumping)
        {
            _dogMoving.SalmonDogJumpMotion();
            rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            isSalmonJumping = false;

            _salmonJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(3));

            _salmonJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }

        // マグロジャンプ
        if (Input.GetKeyDown(KeyCode.RightShift) && isMaguroJumping)
        {
            _dogMoving.MaguroDogJumpMotion();
            rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            isMaguroJumping = false;

            _maguroJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(3));

            _maguroJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }
    }
}
