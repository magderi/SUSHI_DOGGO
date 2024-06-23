using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//  主に寿司犬自身が動く際に使用されるスクリプト
public class DogMoving : MonoBehaviour
{
    public SE_Manager _seManager;


    private DogStatus dogStatus;
    public Rigidbody dogRB;
    [SerializeField]
    private GameObject stand;
    [SerializeField]
    private StandMoving standMoving;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private BoxCollider _playerBoxCollider;

    // 正面に雲があるかの判定用
    [SerializeField]
    private BoxCollider _playerJudgementCollider;

    // アニメーター
    public Animator _sushiSalmonAnim = null;

    public Animator _sushiMaguroAnim = null;

    //  各行動を取っているかの判定フラグ
    public  bool isJumping = false;
    private  bool isJumpCooling = false;
    private bool isCurving = false;

    //  ジャンプに使うクールタイム
    [SerializeField]
    private float _jumpCoolTime = 2f;
    private float _jumpedTimer = 0f;

    //  移動の制限に使うposition値入れ
    private float _dogPosX;
    private float _dogGoToPosX;

    Vector3 standVec;
    Vector3 dogVec;
    float standX;
    float standZ;


    void Start()
    {       

        dogRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();


        standVec = stand.transform.position;
        standX = standVec.x;
        standZ = standVec.z;

        dogVec = this.transform.position;
        dogVec.x = standX;
        dogVec.z = standZ;
    }

    // 寿司犬ダメージ関数
   async public void SalmonDogDamageAnim()
   {
        _sushiSalmonAnim.SetBool("SushiDamage", true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.1));
        _sushiSalmonAnim.SetBool("SushiDamage", false);
        _gameManager._scoreSalmonJudgement = false;
    }
    // 寿司犬ダメージ関数
   async public void MaguroDogDamageAnim()
   {
        _sushiMaguroAnim.SetBool("SushiDamage", true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.1));
        _sushiMaguroAnim.SetBool("SushiDamage", false);
        _gameManager._scoreMaguroJudgement = false;
   }

    // 寿司犬つんのめり関数
    async public void SalmonDogNGJumpAnim()
    {
        _sushiSalmonAnim.SetBool("JumpNG", true);

        _seManager.Play(6);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        _sushiSalmonAnim.SetBool("JumpNG", false);
    
        // _gameManager._scoreSalmonJudgement = false;
    }


    // 寿司犬つんのめり関数
    async public void MaguroDogNGJumpAnim()
    {
        _sushiMaguroAnim.SetBool("JumpNG",true);
       
        _seManager.Play(6);

        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        _sushiMaguroAnim.SetBool("JumpNG", false);
        //_gameManager._scoreMaguroJudgement = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        JumpCoolTime();
    }

    /// <summary>
    /// 寿司犬が連続で飛べないように
    /// </summary>
    private void JumpCoolTime()
    {
        //  クールタイムを過ぎたら、
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  「ジャンプ中」を解除
            isJumpCooling = false;
            _jumpedTimer = 0f;
            standMoving.isJumping = isJumpCooling;
        }

        //  ジャンプしてからの秒数を計る
        if (isJumping && isJumpCooling == false)
        {
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            Debug.Log("ジャンプなう");
            isJumping = false;
            isJumpCooling = true;
        }
        if(isJumpCooling)
        {
            _jumpedTimer += Time.deltaTime;
            return;
        }
    }


    /// <summary>
    /// 寿司犬のジャンプモーション
    /// </summary>
    async public void SalmonDogJumpMotion()
    {
        // アニメーションのトリガーを起動
        _sushiSalmonAnim.SetTrigger("SushiJump");

        // 三秒待機させて連続ジャンプ回避
        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }

    async public void MaguroDogJumpMotion()
    {
        // アニメーションのトリガーを起動
        _sushiMaguroAnim.SetTrigger("SushiJump");

        // 三秒待機させて連続ジャンプ回避
        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }
}
