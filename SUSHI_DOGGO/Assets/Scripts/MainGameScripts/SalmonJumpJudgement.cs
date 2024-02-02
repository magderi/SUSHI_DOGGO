using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class SalmonJumpJudgement : MonoBehaviour
{

    [SerializeField]
    private SushiJump _sushiJump;

    [SerializeField]
    private GameObject _jumpOKText;

    public bool _jumpCoolTime = true;

    //OnTriggerStay関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerStay(Collider other)
    {
        //接触しているオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isSalmonJumping = true;
            _jumpOKText.SetActive(true);
        }
    }

    //OnTriggerExit関数
    //離れたオブジェクトが引数otherとして渡される
    void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isSalmonJumping = false;
            _jumpOKText.SetActive(false);
        }
    }
}
