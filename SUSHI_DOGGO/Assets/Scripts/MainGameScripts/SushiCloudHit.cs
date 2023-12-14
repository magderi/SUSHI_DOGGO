using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SushiCloudHit : MonoBehaviour
{

    [SerializeField]
    private SE_Manager _se_Manager;

    [SerializeField]
    private DogMoving _dogMoving;

    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される

    [SerializeField]
    private GameManager _gameManager;

    //OnTriggerEnter関数
    //接触したオブジェクトが引数otherとして渡される
    void OnTriggerEnter(Collider other)
    {



        // sushiのダメージ処理
        if (other.CompareTag("Sushiinu_salmon"))
        {
            _gameManager.SushiSalmonDamage();

            Debug.Log("コライダー1に当たりました");

            _dogMoving.DogDamageAnim();

            // 流す配列を指定して再生する
            _se_Manager.Play(0);

            //オブジェクトの色を赤に変更する
            // GetComponent<Renderer>().material.color = Color.red;

        }

        // sushiのダメージ処理
        if (other.CompareTag("Sushiinu_Maguro"))
        {
            _gameManager.SushiMaguroDamage();

            Debug.Log("コライダー1に当たりました");

            _dogMoving.DogDamageAnim();

            // 流す配列を指定して再生する
            _se_Manager.Play(0);

            //オブジェクトの色を赤に変更する
            // GetComponent<Renderer>().material.color = Color.red;

        }
    }
}
