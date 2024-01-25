using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalJump : MonoBehaviour
{


    //    [SerializeField]
    // private BoxCollider _goalManager;

    // [SerializeField]
    // private DogMoving _dogMoving;

    [SerializeField]
    private GameObject _goalCamera;

    public Transform target;  // 目標地点のTransform
    public float height = 5f; // 放物線の高さ

    private float startTime;
    private float journeyLength;
    private Vector3 startPos;

    public float speed = 2.0f; // 移動速度

    [SerializeField]
    private Animator _maguroanimator;

    [SerializeField]
    private Animator _salmonanimator;

    [SerializeField]
    private Animator _maguroStandanimator;

    [SerializeField]
    private Animator _salmonStandanimator;

    private bool _goalEnd = false;

    void Start()
    {
        // 初期位置の設定
        startPos = transform.position;

        // 移動の開始時間
        startTime = Time.time;

        // 初期位置から目標地点までの距離
        journeyLength = Vector3.Distance(startPos, target.position);
    }

    void Update()
    {
        if(_goalEnd == false)
        {
            Jump();
        }
        else
        {
            GoalEnd();
        }
        
    
    }

    public void GoalEnd()
    {
        _goalEnd = true;

        _salmonanimator.SetBool("GoalJump", false);
        _maguroanimator.SetBool("GoalJump", false);
    }

    public void Jump()
    {
        _salmonStandanimator.SetTrigger("StandUp");
        _maguroStandanimator.SetTrigger("StandUp");

        _goalCamera.SetActive(true);

        _salmonanimator.SetBool("GoalJump", true);
        _maguroanimator.SetBool("GoalJump", true);

        // 現在の経過時間
        float distCovered = (Time.time - startTime) * speed;

        // 進捗率（0から1の範囲）
        float fracJourney = distCovered / journeyLength;

        // 放物線の計算
        Vector3 currentPos = Vector3.Lerp(startPos, target.position, fracJourney);
        currentPos.y += Mathf.Sin(fracJourney * Mathf.PI) * height;

        // オブジェクトの移動
        transform.position = currentPos;


        // 目標地点に到達したらスクリプトを無効にする
        if (fracJourney >= 1.0f)
        {
            GoalEnd();
            enabled = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalLine"))
        {
           
       
        }
    }
}
