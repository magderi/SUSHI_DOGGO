using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DishScore : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public static class GlobalVariables
    {
        public static int score;
    }

    private void Update()
    {
        GlobalVariables.score = gameManager.score;

        // プレイヤーの操作などでスコアが変わる場合、ここで処理する
        // 例: _scoreIntがスコアを表す場合
        //score ;
    }

}

