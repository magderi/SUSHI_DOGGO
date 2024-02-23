using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DishScore : MonoBehaviour
{
    /// <summary>
    /// メインゲームのDishの値を管理するスクリプトです
    /// </summary>

    [SerializeField]
    private GameManager gameManager;

    public static class GlobalVariables
    {
        // ここのスコアの値を変更するとリザルトシーンでのデバックが出来ます。（例 score = 100　等）
        public static int score;
    }

    private void Update()
    {
        GlobalVariables.score = gameManager.score;
    }
}

