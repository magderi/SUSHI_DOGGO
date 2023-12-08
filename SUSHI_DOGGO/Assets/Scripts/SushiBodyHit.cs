using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SushiBodyHit : MonoBehaviour
{

    [SerializeField]
    private DogMoving _dogMoving;

    //OnTriggerEnter�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����

    [SerializeField]
    private GameManager _gameManager;

    //OnTriggerEnter�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerEnter(Collider other)
    {



        // �ڐG�����I�u�W�F�N�g�̃^�O��"Cloud"�̂Ƃ������������s��
        if (other.CompareTag("Player"))
        {
            _gameManager.SushiDamage();

            Debug.Log("�R���C�_�[1�ɓ�����܂���");

            _dogMoving.DogDamageAnim();

            //�I�u�W�F�N�g�̐F��ԂɕύX����
            // GetComponent<Renderer>().material.color = Color.red;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
