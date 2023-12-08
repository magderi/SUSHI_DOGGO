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

    //OnTriggerStay�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerStay(Collider other)
    {
        //�ڐG���Ă���I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Cloud") && _jumpCoolTime)
        {
            _sushiJump.isJumping = true;
            _jumpOKText.SetActive(true);

           

            Debug.Log("JumpTrue");


      

        }
    }

    //OnTriggerExit�֐�
    //���ꂽ�I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerExit(Collider other)
    {
        //���ꂽ�I�u�W�F�N�g�̃^�O��"Player"�̂Ƃ�
        if (other.CompareTag("Cloud"))
        {
            _sushiJump.isJumping = false;
            _jumpOKText.SetActive(false);
            Debug.Log("JumpFalse");
        }
    }
}
