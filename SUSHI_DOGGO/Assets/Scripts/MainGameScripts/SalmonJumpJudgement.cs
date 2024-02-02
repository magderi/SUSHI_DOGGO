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

    //OnTriggerStay�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerStay(Collider other)
    {
        //�ڐG���Ă���I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isSalmonJumping = true;
            _jumpOKText.SetActive(true);
        }
    }

    //OnTriggerExit�֐�
    //���ꂽ�I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerExit(Collider other)
    {
        //���ꂽ�I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isSalmonJumping = false;
            _jumpOKText.SetActive(false);
        }
    }
}
