using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScoreMove : MonoBehaviour
{
    public float targetYRotation = 100f; // 目標のy軸回転角度
    public float rotationSpeed = 90f; // 回転速度

    private bool isRotating = false; // 回転中かどうかのフラグ
    public bool _scorejoyMove = false;

    public Transform target; // 目標位置

    private float speed = 0.5f; // 移動速度（任意の値に調整してください）
    private float startTime; // 移動開始時間

    void Start()
    {
        
    }

    void RotateToTarget()
    {
        // オブジェクトの現在のRotationを取得
        Quaternion currentRotation = transform.rotation;

        // 目標のRotationを計算
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        // 回転の開始
        StartCoroutine(RotateCoroutine(currentRotation, targetRotation));
    }


    void Update()
    {
        if (_scorejoyMove == true)
        {
            startTime = Time.time; // 移動開始時間を記録

            // 目標位置に向かって移動する方向ベクトルを計算
            Vector3 direction = (target.position - transform.position).normalized;

            // 目標位置に向かって一定の速度で移動
            transform.position += direction * speed * Time.deltaTime;

            // 5秒後に移動を停止
            if (Time.time - startTime >= 5f)
            {
                enabled = false; // スクリプトを無効にすることで移動を停止
            }
        }
    }

    IEnumerator RotateCoroutine(Quaternion startRotation, Quaternion targetRotation)
    {
      
        // 回転中のフラグを立てる
        isRotating = true;

        // 回転開始時間
        float startTime = Time.time;

        // 回転が完了するまでの経過時間
        float journeyLength = Quaternion.Angle(startRotation, targetRotation);

        while (isRotating)
        {
            // 現在の経過時間
            float elapsedTime = Time.time - startTime;

            // 回転の進行度合いを計算
            float fractionOfJourney = Mathf.Clamp01(elapsedTime * 1f); // 1秒かけて回転する

            // 回転を補間
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);

            // 目標に到達したかどうかをチェック
            if (fractionOfJourney >= 1f)
            {
                // 回転完了
                isRotating = false;
               
            }

            // 1フレーム待機
            yield return null;
        }




    }

    public void ScoreJoyMove()
    {
        // 開始時に回転を開始
        RotateToTarget();
        _scorejoyMove = true;

        // オブジェクトのRotationを取得
        Vector3 rotation = transform.rotation.eulerAngles;

        // y軸のRotationを100に設定
        rotation.y = 100f;

        // 新しいRotationを適用
        transform.rotation = Quaternion.Euler(rotation);

    }
}
