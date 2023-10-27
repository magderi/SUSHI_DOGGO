using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;//Unity����Player���i�[
    public GameObject _floorPos;

    public GameObject[] grounds = new GameObject[3];//Unity���琶������Ground��Prefab���A�^�b�`

    float border = 15;
    float playerStartPosZ;//Player�̏���x���W
    float playerNowPosZ;//Player�̌���x���W

    bool _stageGenerat = true;

    void Start()
    {
        player = GameObject.Find("Player");//Hierarcy�̒����疼�O��"Player"�̂��̂�T���ė��Ď擾���ϐ�player�Ɋi�[

        playerStartPosZ = player.transform.position.z;//�ŏ��̊�l�ƂȂ�Player��
    }

    void Update()
    {
        //���W������������
        transform.position += new Vector3(0, 0, -5) * Time.deltaTime;
        GenerateGround();//Ground�����̊Ԋu�Ő���
    }

    //Ground�����̊Ԋu�Ő���
    void GenerateGround()
    {
        if (Mathf.Floor(_floorPos.transform.position.z) >= 40)
        {
            //�X�e�[�W����
            Debug.Log("�X�e�[�W����");
            Instantiate(grounds[Random.Range(0, 3)], new Vector3(0, 0, _floorPos.transform.position.z + 80), Quaternion.identity);//Player�̈�苗��������ɃX�e�[�W����(-5.5f�̓X�e�[�W�����̈ʒu�␳�̈�)

            border = 10;//border�̍Đݒ�
        }
    }
}
