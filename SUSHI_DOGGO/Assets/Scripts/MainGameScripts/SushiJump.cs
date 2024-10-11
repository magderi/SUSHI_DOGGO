using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// 寿司のジャンプに関する機能を管理するクラス
/// </summary>
public class SushiJump : MonoBehaviour
{
    // public float jumpPower;
    private Rigidbody rb;

    // サーモンのジャンプ状態を管理
    public bool isSalmonJumping = false;
    public bool isMaguroJumping = false;

    // アニメーションの管理用
    [SerializeField]
    private DogMoving _dogMoving;

    // サーモンジャンプの判定用
    [SerializeField]
    private SalmonJumpJudgement _salmonJumpJudgement;
    [SerializeField]
    private MaguroJumpJudgement _maguroJumpJudgement;

    [SerializeField]
    private BoxCollider _boxCollider;

    // ジャンプ時に発生するエフェクト
    [SerializeField]
    private ParticleSystem _jumpSalmonParticle;
    [SerializeField]
    private ParticleSystem _jumpMaguroParticle;

    [SerializeField]
    private SE_Manager _seManager;

    // フラグを管理することで、連続ジャンプの防止
    public bool isSalmonJump = false;
    public bool isMaguroJump = false;

    void Start()
    {
        _jumpSalmonParticle.Stop();
        _jumpMaguroParticle.Stop();
        rb = GetComponent<Rigidbody>();
    }

    // サーモンジャンプ時のエフェクトとSEの再生
    public void SalmonDogJumpParticle()
    {
        _jumpSalmonParticle.Play();

        // エフェクト音
        _seManager.Play(2);

        // ジャンプ音
        _seManager.Play(4);
    }

    // マグロジャンプ時のエフェクトとSEの再生
    public void MaguroDogJumpParticle()
    {
        _jumpMaguroParticle.Play();

        // エフェクト音
        _seManager.Play(2);

        // ジャンプ音
        _seManager.Play(3);
    }

    async void Update()
    {
        // サーモンのジャンプ
        if (isSalmonJump && isSalmonJumping)
        {
            _dogMoving.SalmonDogJumpMotion();


            // ジャンプ終了後、クールダウン時間をリセット
            isSalmonJumping = false;

            _salmonJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.0));

            _salmonJumpJudgement._jumpCoolTime = true;
        }
        else if (isSalmonJump)
        {
            _dogMoving.SalmonDogNGJumpAnim();
        }

        // マグロのジャンプ
        if (isMaguroJump && isMaguroJumping)
        {
            _dogMoving.MaguroDogJumpMotion();

            // ジャンプ終了後、クールダウン時間をリセット
            isMaguroJumping = false;

            _maguroJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.0));

            _maguroJumpJudgement._jumpCoolTime = true;
        }
        else if (isMaguroJump)
        {
            _dogMoving.MaguroDogNGJumpAnim();
        }

    }
}
