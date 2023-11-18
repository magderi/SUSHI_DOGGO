using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DogMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    public Rigidbody dogRB;

    //  各行動を取っているかの判定フラグ
    public  bool isJumping = false;
    private bool isCurving = false;
    private bool isRightMoving = false;
    private bool isLeftMoving = false;

    //  今いるレーンを判定するためのint値
    //  0が一番左、5が一番右の6レーン
    private int _laneNamber;

    //  ジャンプに使うクールタイム
    [SerializeField]
    private float _jumpCoolTime = 1.0f;
    private float _jumpedTimer = 0f;

    //  移動の制限に使うposition値入れ
    private float _dogPosX;
    private float _dogGoToPosX;

    // Start is called before the first frame update
    void Start()
    {
        dogRB = GetComponent<Rigidbody>();
        dogStatus = GetComponent<DogStatus>();
        
        //  開始時の寿司犬の position.x を取得
        _dogPosX = transform.position.x;
        _dogGoToPosX = _dogPosX;

        //  仮に、左から3番目に置いてある想定
        _laneNamber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        JumpCoolTime();
        PlayerMove();
          
        //  「カーブ中」なら、
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && isJumping == false)
        {
            //  statusで設定してある力の分、上に飛ぶ
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            //  「ジャンプ中」に設定
            isJumping = true;
        }
    }
    
    public void MoveRight(InputAction.CallbackContext context)
    {   
        if (context.phase == InputActionPhase.Performed)
        {
            //  X座標を右に10だけ移動
            _dogGoToPosX += 10.0f;
            //  「右に移動中」に設定
            isRightMoving = true;
            //  レーンナンバーをインクリメント
            _laneNamber++;
        }
        else if (context.phase == InputActionPhase.Canceled)
            //  入力を離したら「右に移動中」を解除
            isRightMoving = false;
    }

    public void MoveLeft(InputAction.CallbackContext context)
    {    
        if (context.phase == InputActionPhase.Performed)
        {
            //  X座標を左に10だけ移動
            _dogGoToPosX -= 10.0f;
            //  「左に移動中」に設定
            isLeftMoving = true;
            //  レーンナンバーをデクリメント
            _laneNamber--;
        }
        else if (context.phase == InputActionPhase.Canceled)
            //  入力を離したら「左に移動中」を解除
            isLeftMoving = false;
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
            isJumping = false;
            _jumpedTimer = 0f;
        }

        //  ジャンプしてからの秒数を計る
        if (isJumping)
        {
            _jumpedTimer += Time.deltaTime;
            return;
        }
    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        //  「左に移動中」なら、
        if (isLeftMoving)
        {
            //  設定した移動先に到達したら
            if(_dogPosX <= _dogGoToPosX)
            {
                //  velocity を 0 にして終わる
                dogRB.velocity = Vector3.left * 0;
                return;
            }
            //  今の position の x を保存して、左に力を加える
            _dogPosX = transform.position.x;
            //  AddForce で物理挙動をさせるか、 velocity でアニメチックな動きにするか
            //dogRB.AddForce(Vector3.left * dogStatus._movePower);
            dogRB.velocity = Vector3.left * dogStatus._jumpPower;
        }
        //  「右に移動中」なら、
        if (isRightMoving)
        {
            //  設定した移動先に到達したら
            if (_dogPosX >= _dogGoToPosX)
            {
                //  velocity を 0 にして終わる;
                dogRB.velocity = Vector3.right * 0;
                return;
            }
            //  今の position の x を保存して、右に力を加える
            _dogPosX = transform.position.x;
            //dogRB.AddForce(Vector3.right * dogStatus._movePower);
            dogRB.velocity = Vector3.right * dogStatus._movePower;
        }
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
