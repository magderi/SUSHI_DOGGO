using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DishScore : MonoBehaviour
{
    // スコア
    public static int score;

    public int _scoreInt;
    private void Update()
    {
        // プレイヤーの操作などでスコアが変わる場合、ここで処理する
        // 例: _scoreIntがスコアを表す場合
        score = _scoreInt;
    }

    private void OnDestroy()
    {
        // シーンが破棄される時にスコアを保存
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}

