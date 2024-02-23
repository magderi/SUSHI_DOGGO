using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiObstacleHit : MonoBehaviour
{
    /// <summary>
    /// ���C���Q�[���Ŏ��i������Q���ɓ��������ۂ̏����ł�
    /// </summary>

    // �ȉ��Q��
    [SerializeField]
    private SE_Manager  _se_Manager;

    [SerializeField]
    private DogMoving �@_dogMoving;

    [SerializeField]
    private GameManager _gameManager;

    //OnTriggerEnter�֐�
    //�ڐG�����I�u�W�F�N�g������other�Ƃ��ēn�����
    void OnTriggerEnter(Collider other)
    {
        // sushi�̃_���[�W����
        if (other.CompareTag("Sushiinu_salmon"))
        {
            _gameManager.SushiSalmonDamage();

            Debug.Log("�R���C�_�[�ɓ�����܂���");

            _dogMoving.SalmonDogDamageAnim();

            // �����z����w�肵�čĐ�����
            _se_Manager.Play(5);

            //�I�u�W�F�N�g�̐F��ԂɕύX����
            // GetComponent<Renderer>().material.color = Color.red;

        }

        // sushi�̃_���[�W����
        if (other.CompareTag("Sushiinu_Maguro"))
        {
            _gameManager.SushiMaguroDamage();

            Debug.Log("�R���C�_�[�ɓ�����܂���");

            _dogMoving.MaguroDogDamageAnim();

            // �����z����w�肵�čĐ�����
            _se_Manager.Play(0);

            //�I�u�W�F�N�g�̐F��ԂɕύX����
            // GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
