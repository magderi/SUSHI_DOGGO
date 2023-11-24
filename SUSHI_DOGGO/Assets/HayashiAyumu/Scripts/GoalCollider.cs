using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("goal!!");
        SceneManager.LoadScene("score");
    }
}
