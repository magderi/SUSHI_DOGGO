using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCollider : MonoBehaviour
{
    //  ゴールのコライダーに当たった時の処理
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("goal!!");
        SceneManager.LoadScene("score");
    }
}
