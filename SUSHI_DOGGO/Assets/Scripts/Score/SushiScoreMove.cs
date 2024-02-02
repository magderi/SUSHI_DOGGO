using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScoreMove : MonoBehaviour
{
    //回転中かどうか
    bool coroutineBool = false;

    public bool _spinStart = false;

    void Update()
    {
        /*
        if (_spinStart == true)
        {
            //回転中ではない場合は実行 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("RightMove");
            }
        }
        */

        if (_spinStart == true)
        {
            //回転中ではない場合は実行 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("LeftMove");
            }
        }
    }

    //右にゆっくり回転して90°でストップ
    IEnumerator RightMove()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
    }

    //左にゆっくり回転して90°でストップ
    IEnumerator LeftMove()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        //座標を書き換える
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
    }

    public void MoveLeft()
    {
        _spinStart = true;
    }
}
