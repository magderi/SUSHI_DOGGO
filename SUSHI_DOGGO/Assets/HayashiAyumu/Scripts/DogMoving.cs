using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMoving : MonoBehaviour
{
    private DogStatus dogStatus;
    protected Rigidbody dogRB;

    //  各行動を取っているかの判定フラグ
    private bool isJumping = false;
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
        PlayerJump();
        PlayerMove();
          
        //  「カーブ中」なら、
        if(isCurving)
            CurveMoveLimit(dogStatus._maxMoveLimit);
    }

    /// <summary>
    /// 寿司犬のジャンプ処理
    /// </summary>
    private void PlayerJump()
    {
        //  クールタイム中なら、
        if (_jumpedTimer >= _jumpCoolTime)
        {
            //  「ジャンプ中」を解除
            isJumping = false;
            _jumpedTimer = 0f;
        }
        if (isJumping)
        {
            //  ジャンプしてからの秒数を計る
            _jumpedTimer += Time.deltaTime;
            return;
        }
        //  一先ずスペースでジャンプ
        //  後々 InputSystem に切り替え
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //  statusで設定してある力の分、上に飛ぶ
            dogRB.AddForce(Vector3.up * dogStatus._jumpPower);
            //  「ジャンプ中」に設定
            isJumping = true;
        }

    }

    /// <summary>
    /// 寿司犬の左右の移動処理
    /// </summary>
    private void PlayerMove()
    {
        //  Aキーで左に移動
        //  ジャンプ同様後々 InputSystem に切り替え
        if (Input.GetKeyDown(KeyCode.A))
        {
            //  X座標を左に10だけ移動
            _dogGoToPosX -= 10.0f;
            //  「左に移動中」に設定
            isLeftMoving = true;
            //  レーンナンバーをデクリメント
            _laneNamber--;
        }
        else if (Input.GetKeyUp(KeyCode.A))
            //  入力を離したら「左に移動中」を解除
            isLeftMoving = false;

        //  Dキーで右に移動
        //  ジャンプ同様後々 InputSystem に切り替え
        if (Input.GetKeyDown(KeyCode.D))
        {
            //  X座標を右に10だけ移動
            _dogGoToPosX += 10.0f;
            //  「右に移動中」に設定
            isRightMoving = true;
            //  レーンナンバーをインクリメント
            _laneNamber++;
        }
        else if (Input.GetKeyUp(KeyCode.D))
            //  入力を離したら「右に移動中」を解除
            isRightMoving = false;


        //  「左に移動中」なら、
        if (isLeftMoving)
        {
            //  設定した移動先に到達したら
            if(_dogPosX <= _dogGoToPosX)
            {
                //  velocity を 0 にして終わる
                dogRB.velocity = Vector3.zero;
                return;
            }
            //  今の position の x を保存して、左に力を加える
            _dogPosX = transform.position.x;
            dogRB.AddForce(Vector3.left * dogStatus._movePower);
        }
        //  「右に移動中」なら、
        if (isRightMoving)
        {
            //  設定した移動先に到達したら
            if (_dogPosX >= _dogGoToPosX)
            {
                //  velocity を 0 にして終わる;
                return;
            }
            //  今の position の x を保存して、右に力を加える
            _dogPosX = transform.position.x;
            dogRB.AddForce(Vector3.right * dogStatus._movePower);
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
