using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public Image[] imagesToShow;
    public Text scoreText;
    public AudioSource bgmAudioSource;
    private int targetScore = 200;
    private float scoreIncreaseDuration = 2f;

    void Start()
    {
        // 全ての画像とスコアテキストを初期状態で非表示にする
        foreach (Image image in imagesToShow)
        {
            image.gameObject.SetActive(false);
        }
        scoreText.gameObject.SetActive(false);

        // ランダムにターゲットスコアを0〜200の範囲で設定
        targetScore = Random.Range(0, 201);

        // 1秒後に最初の画像を表示する
        Invoke("ShowImage0", 1f);
    }

    // 最初の画像を表示するメソッド
    void ShowImage0()
    {
        imagesToShow[0].gameObject.SetActive(true); // 配列の0番目の画像を表示
        imagesToShow[7].gameObject.SetActive(true); // 配列の7番目の画像を表示

        // 1秒後にスコアを表示する
        Invoke("ShowScore", 1f);
    }

    // スコアを表示するメソッド
    void ShowScore()
    {
        scoreText.gameObject.SetActive(true);  // スコアテキストを表示
        StartCoroutine(IncreaseScoreOverTime());  // スコアを時間経過で増加させるコルーチンを開始
    }

    // スコアを徐々に増加させるコルーチン
    IEnumerator IncreaseScoreOverTime()
    {
        float elapsedTime = 0f;
        int currentScore = 0;

        // 指定された時間内でスコアを徐々に増加させる
        while (elapsedTime < scoreIncreaseDuration)
        {
            currentScore = (int)Mathf.Lerp(0, targetScore, elapsedTime / scoreIncreaseDuration);
            scoreText.text = currentScore.ToString(); // スコアテキストを更新

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終的なターゲットスコアを表示
        scoreText.text = targetScore.ToString();

        // 1秒後にスコアに応じた画像を表示
        Invoke("ShowImagesByScore", 1f);
    }

    // スコアに応じた画像を表示
    void ShowImagesByScore()
    {
        if (targetScore < 100)
        {
            // スコアが100未満の場合、1番目の画像のみ表示
            imagesToShow[1].gameObject.SetActive(true);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // スコアが100〜149の場合、2番目の画像のみ表示
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(true);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // スコアが150以上の場合、3番目の画像のみ表示
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(true);
        }
        PlayBGM();  // BGMを再生
    }

    // BGMを再生
    void PlayBGM()
    {
        if (bgmAudioSource != null && bgmAudioSource.clip != null)
        {

            bgmAudioSource.Play();
        }

        // 1秒後にさらに画像をスコアに応じて表示
        Invoke("ShowImagesByScore1", 1f);
    }

    // スコアに応じてさらに画像を表示
    void ShowImagesByScore1()
    {
        if (targetScore < 100)
        {
            // スコアが100未満の場合、4番目の画像のみ表示
            imagesToShow[4].gameObject.SetActive(true);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // スコアが100〜149の場合、5番目の画像のみ表示
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(true);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // スコアが150以上の場合、6番目の画像のみ表示
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(true);
        }
    }
}
