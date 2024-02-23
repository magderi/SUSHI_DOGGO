using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaguroJumpJudgement : MonoBehaviour
{

    /// <summary>
    /// マグロのジャンプを管理するスクリプトです
    /// </summary>

    // 以下参照
    [SerializeField]
    private SushiJump   _sushiJump;

    [SerializeField]
    private GameObject  _jumpOKMaguroText;

    public bool         _jumpCoolTime = true;


    //OnTriggerStay関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerStay(Collider other)
    {
        //接触しているオブジェクトのタグが"Cloud"のとき
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isMaguroJumping = true;
            _jumpOKMaguroText.SetActive(true);

            Debug.Log("JumpTrue");
        }
    }

    //OnTriggerExit関数
    //離れたオブジェクトが引数otherとして渡される
    void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトのタグが"Cloud"のとき
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isMaguroJumping = false;
            _jumpOKMaguroText.SetActive(false);
            Debug.Log("JumpFalse");
        }
    }
}
