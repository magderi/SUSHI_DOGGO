using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaguroJumpJudgement : MonoBehaviour
{
    [SerializeField]
    private SushiJump _sushiJump;

    [SerializeField]
    private GameObject _jumpOKMaguroText;

    public bool _jumpCoolTime = true;



    void OnTriggerStay(Collider other)
    {
        //雲に接触したらOK表示
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isMaguroJumping = true;
            _jumpOKMaguroText.SetActive(true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        //雲に接触したらOK非表示
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isMaguroJumping = false;
            _jumpOKMaguroText.SetActive(false);
        }
    }
}
