using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;//UnityからPlayerを格納
    public GameObject[] grounds = new GameObject[3];//Unityから生成するGroundのPrefabをアタッチ

    float border = 15;
    float playerStartPosZ;//Playerの初期x座標
    float playerNowPosZ;//Playerの現在x座標



    void Start()
    {
        player = GameObject.Find("Player");//Hierarcyの中から名前が"Player"のものを探して来て取得→変数playerに格納

        playerStartPosZ = player.transform.position.z;//最初の基準値となるPlayerの
    }

    void Update()
    {
        GenerateGround();//Groundを一定の間隔で生成
    }

    //Groundを一定の間隔で生成
    void GenerateGround()
    {
        playerNowPosZ = player.transform.position.z;//Playerの現在x座標を変数playerNowPosXに格納
        float playerDistance = playerNowPosZ - playerStartPosZ;//Playerの移動距離(playerNowPosXとplayerStartPosXの差分)を変数playerDistanceに格納
        if (playerDistance > border)
        {
            //ステージ生成
            Debug.Log("ステージ生成");
            Instantiate(grounds[Random.Range(0, 3)], new Vector3(0, 0, player.transform.position.z + 40), Quaternion.identity);//Playerの一定距離だけ先にステージ生成(-5.5fはステージ生成の位置補正の為)
            playerDistance = 0;//playerDistanceのリセット
            border = 10;//borderの再設定
            playerStartPosZ = playerNowPosZ;//playerStartPosの再設定
        }
    }
}
