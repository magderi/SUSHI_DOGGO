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
    private StandMoving standMoving;

    //  各行動を取っているかの判定フラグ
    public  bool isJumping = false;
    private  bool isJumpCooling = false;
    private bool isCurving = false;

    //  ジャンプに使うクールタイム
    [SerializeField]
    private float _jumpCoolTime = 1.0f;
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


        //standVec = stand.transform.position;
        //standX = standVec.x;
        //standZ = standVec.z;

        //dogVec = this.transform.position;
        //dogVec = new Vector3(standX, dogVec.y, standZ);
        //this.transform.position = dogVec;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        JumpCoolTime();
        //DogMove();
          
        //  「カーブ中」なら、
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    /// <summary>
    /// 寿司犬が連続で飛べないように
    /// </summary>
    private void JumpCoolTime()
    {
        //  クールタイム中なら、
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  「ジャンプ中」を解除
            isJumpCooling = false;
            _jumpedTimer = 0f;
            standMoving.isJumping = isJumpCooling;
        }

        //  ジャンプしてからの秒数を計る
        if (isJumping)
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
    /// 寿司犬の左右の移動処理
    /// </summary>
    //private void DogMove()
    //{
    //    standVec = stand.transform.position;
    //    standX = standVec.x;
    //    standZ = standVec.z;

    //    dogVec.x = standX;
    //    dogVec.z = standZ;
    //    this.transform.position = dogVec;
    //}

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
