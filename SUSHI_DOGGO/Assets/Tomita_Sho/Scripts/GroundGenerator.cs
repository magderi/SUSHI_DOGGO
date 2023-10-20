using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;//Unity����Player���i�[
    public GameObject[] grounds = new GameObject[3];//Unity���琶������Ground��Prefab���A�^�b�`

    float border = 15;
    float playerStartPosZ;//Player�̏���x���W
    float playerNowPosZ;//Player�̌���x���W



    void Start()
    {
        player = GameObject.Find("Player");//Hierarcy�̒����疼�O��"Player"�̂��̂�T���ė��Ď擾���ϐ�player�Ɋi�[

        playerStartPosZ = player.transform.position.z;//�ŏ��̊�l�ƂȂ�Player��
    }

    void Update()
    {
        GenerateGround();//Ground�����̊Ԋu�Ő���
    }

    //Ground�����̊Ԋu�Ő���
    void GenerateGround()
    {
        playerNowPosZ = player.transform.position.z;//Player�̌���x���W��ϐ�playerNowPosX�Ɋi�[
        float playerDistance = playerNowPosZ - playerStartPosZ;//Player�̈ړ�����(playerNowPosX��playerStartPosX�̍���)��ϐ�playerDistance�Ɋi�[
        if (playerDistance > border)
        {
            //�X�e�[�W����
            Debug.Log("�X�e�[�W����");
            Instantiate(grounds[Random.Range(0, 3)], new Vector3(0, 0, player.transform.position.z + 40), Quaternion.identity);//Player�̈�苗��������ɃX�e�[�W����(-5.5f�̓X�e�[�W�����̈ʒu�␳�̈�)
            playerDistance = 0;//playerDistance�̃��Z�b�g
            border = 10;//border�̍Đݒ�
            playerStartPosZ = playerNowPosZ;//playerStartPos�̍Đݒ�
        }
    }
}
