using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
/// <summary>
/// 寿司犬のジャンプに関する処理を持つ関数
/// アニメーションのトリガーはDogMovingスクリプトで発火してます
/// </summary>
public class SushiJump : MonoBehaviour
{
  //  public float jumpPower;
    private Rigidbody rb;

    // 同時着地の制作途中の名残、試遊会では使ってない
    public bool isSalmonJumping = false;
    public bool isMaguroJumping = false;

    // アニメーションはここでさせる
    [SerializeField]
    private DogMoving _dogMoving;

    // 同時着地の制作途中の名残、試遊会では使ってない
    [SerializeField]
    private SalmonJumpJudgement _salmonJumpJudgement;
    [SerializeField]
    private MaguroJumpJudgement _maguroJumpJudgement;

    [SerializeField]
    private BoxCollider _boxCollider;

    // ジャンプ成功時にFX再生
    [SerializeField]
    private ParticleSystem _jumpSalmonParticle;
    [SerializeField]
    private ParticleSystem _jumpMaguroParticle;

    [SerializeField]
    private SE_Manager _seManager;
    void Start()
    {
        _jumpSalmonParticle.Stop();

        _jumpMaguroParticle.Stop();

        rb = GetComponent<Rigidbody>();
    }
    // サーモンのFX再生＆SEの再生
    public void SalmonDogJumpParticle()
    {
        _jumpSalmonParticle.Play();

        // FXのSE
        _seManager.Play(2);

        // 犬のSE
        _seManager.Play(4);
    }

    public void MaguroDogJumpParticle()
    {
        _jumpMaguroParticle.Play();

        // FXのSE
        _seManager.Play(2);

        // 犬のSE
        _seManager.Play(3);
    }

    async void Update()
    {
        // サーモンジャンプ
        if (Input.GetKeyDown(KeyCode.LeftShift) && isSalmonJumping)
        {

            // DogMovingの関数を使用
            _dogMoving.SalmonDogJumpMotion();

            // rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));

            // 以下ジャンプのクールタイム処理
            isSalmonJumping = false;

            
            _salmonJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _salmonJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }

        // マグロジャンプ
        if (Input.GetKeyDown(KeyCode.RightShift) && isMaguroJumping)
        {

            // DogMovingの関数を使用
            _dogMoving.MaguroDogJumpMotion();

            // rb.velocity = Vector3.up * jumpPower;
            //await UniTask.Delay(TimeSpan.FromSeconds(1));
            // 以下ジャンプのクールタイム処理
            isMaguroJumping = false;

            
            _maguroJumpJudgement._jumpCoolTime = false;

            await UniTask.Delay(TimeSpan.FromSeconds(1.5));

            _maguroJumpJudgement._jumpCoolTime = true;


            Debug.Log("test");
        }
    }   

}
