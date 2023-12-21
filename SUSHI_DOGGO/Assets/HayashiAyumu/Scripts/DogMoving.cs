using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody dogRB;
    [SerializeField]
    private GameObject stand;
    [SerializeField]
    private StandMoveController dogController;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private BoxCollider _playerBoxCollider;

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



    // Start is called before the first frame update
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

   public void SalmonDogDamageAnim()
   {
        _sushiSalmonAnim.SetTrigger("SushiDamage");

        _gameManager._scoreSalmonJudgement = false;
    }

   public void MaguroDogDamageAnim()
   {
        _sushiMaguroAnim.SetTrigger("SushiDamage");

        _gameManager._scoreMaguroJudgement = false;
   }

    private void Update()
    {


    }

    // Update is called once per frame
    void LateUpdate()
    {

     

        JumpCoolTime();
        DogMove();
          
        //  「カーブ中」なら、
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
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
            dogController.isJumping = isJumpCooling;
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
        _sushiSalmonAnim.SetTrigger("SushiJump");

       _gameManager._scoreSalmonJudgement = true;


       _gameManager._scoreMaguroJudgement = true;
        Debug.Log("iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");

  

        await UniTask.Delay(TimeSpan.FromSeconds(3));

  

        //JudgementScore();

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        _gameManager._scoreSalmonJudgement = false;
        
    }

    async public void MaguroDogJumpMotion()
    {
        _sushiMaguroAnim.SetTrigger("SushiJump");

        _gameManager._scoreSalmonJudgement = true;


        _gameManager._scoreMaguroJudgement = true;

      
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa;");
        
        await UniTask.Delay(TimeSpan.FromSeconds(3));

        _gameManager._scoreSalmonJudgement = false;

        _gameManager._scoreMaguroJudgement = false;

        //JudgementScore();

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        _gameManager._scoreMaguroJudgement = false;
        
    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void DogMove()
    {
        standVec = stand.transform.position;
        standX = standVec.x;
        standZ = standVec.z;

        this.transform.position = new Vector3 (standX, this.transform.position.y, standZ);
    }

    /// <summary>
    /// カーブ時の処理(未完成)
    /// </summary>
    /// <param name="MaxMoveLimit"></param>
    private void CurveMoveLimit(float MaxMoveLimit)
    {
        //  移動速度に制限をつけて滑らかに動かそうとしている...はず？
        float _currentMoveSpeed = dogRB.velocity.z;
        if(_currentMoveSpeed > MaxMoveLimit)
        {
            _currentMoveSpeed /= MaxMoveLimit;
            dogRB.velocity = new Vector3(
                dogRB.velocity.x,
                dogRB.velocity.y, 
                _currentMoveSpeed);
        }
    }

  

}
