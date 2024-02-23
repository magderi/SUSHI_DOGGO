using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class SalmonJumpJudgement : MonoBehaviour
{
    /// <summary>
    /// �T�[�����̃W�����v���Ǘ�����X�N���v�g�ł�
    /// </summary>

    // �ȉ��Q��
    [SerializeField]
    private SushiJump   _sushiJump;

    [SerializeField]
    private GameObject  _jumpOKText;

    public bool         _jumpCoolTime = true;

    //OnTriggerStay�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerStay(Collider other)
    {
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
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isSalmonJumping = false;
            _jumpOKText.SetActive(false);
        }
    }
}
