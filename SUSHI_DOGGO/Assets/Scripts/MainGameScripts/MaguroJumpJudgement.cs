using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaguroJumpJudgement : MonoBehaviour
{

    /// <summary>
    /// �}�O���̃W�����v���Ǘ�����X�N���v�g�ł�
    /// </summary>

    // �ȉ��Q��
    [SerializeField]
    private SushiJump   _sushiJump;

    [SerializeField]
    private GameObject  _jumpOKMaguroText;

    public bool         _jumpCoolTime = true;


    //OnTriggerStay�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerStay(Collider other)
    {
        //�ڐG���Ă���I�u�W�F�N�g�̃^�O��"Cloud"�̂Ƃ�
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isMaguroJumping = true;
            _jumpOKMaguroText.SetActive(true);

            Debug.Log("JumpTrue");
        }
    }

    //OnTriggerExit�֐�
    //���ꂽ�I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerExit(Collider other)
    {
        //���ꂽ�I�u�W�F�N�g�̃^�O��"Cloud"�̂Ƃ�
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isMaguroJumping = false;
            _jumpOKMaguroText.SetActive(false);
            Debug.Log("JumpFalse");
        }
    }
}
