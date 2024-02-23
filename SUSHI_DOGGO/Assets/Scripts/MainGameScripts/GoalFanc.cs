using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFanc : MonoBehaviour
{
    /// <summary>
    /// ゴール手前になったら起動するスクリプトです
    /// </summary>


    // 以下それぞれ参照
    [SerializeField]
    private GoalJump    _goalJump;

    [SerializeField]
    private SushiJump   _shushiJump;

    [SerializeField]
    private StandMoving _standMoving;

    // ゴールラインに接触時に起動
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalLine"))
        {
            _goalJump.enabled = true;

            _shushiJump.enabled = false;

            _standMoving.enabled = false;
        }
    }
}
