using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SushiBodyHit : MonoBehaviour
{

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



        // 接触したオブジェクトのタグが"Cloud"のときだけ処理を行う
        if (other.CompareTag("Player"))
        {
            _gameManager.SushiDamage();

            Debug.Log("コライダー1に当たりました");

            _dogMoving.DogDamageAnim();

            //オブジェクトの色を赤に変更する
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
