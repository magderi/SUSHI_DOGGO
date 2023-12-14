using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class JumpJudgement : MonoBehaviour
{

    [SerializeField]
    private SushiJump _sushiJump;

    [SerializeField]
    private GameObject _jumpOKText;

    public bool _jumpCoolTime = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //OnTriggerStay関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerStay(Collider other)
    {
        //接触しているオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isJumping = true;
            _jumpOKText.SetActive(true);

           

            Debug.Log("JumpTrue");


      

        }
    }

    //OnTriggerExit関数
    //離れたオブジェクトが引数otherとして渡される
    void OnTriggerExit(Collider other)
    {
        //離れたオブジェクトのタグが"Player"のとき
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isJumping = false;
            _jumpOKText.SetActive(false);
            Debug.Log("JumpFalse");
        }
    }
}
