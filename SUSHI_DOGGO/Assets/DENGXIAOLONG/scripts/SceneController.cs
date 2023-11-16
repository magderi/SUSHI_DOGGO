using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class SceneController : MonoBehaviour
{
    public Image imageToShow;
    public Text scoreText;
    private int targetScore = 200;
    private float scoreIncreaseDuration = 2f;

    void Start()
    {
        // 隐藏图片和分数文本
        imageToShow.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        targetScore = Random.Range(0, 201);

        // 延迟1秒后显示图片
        Invoke("ShowImage", 1f);
    }

    void ShowImage()
    {
        // 显示图片
        imageToShow.gameObject.SetActive(true);

        // 延迟1秒后显示分数文本
        Invoke("ShowScore", 1f);
    }

    void ShowScore()
    {
        // 显示分数文本
        scoreText.gameObject.SetActive(true);

        // 启动分数增加的协程
        StartCoroutine(IncreaseScoreOverTime());
    }

    IEnumerator IncreaseScoreOverTime()
    {
        float elapsedTime = 0f;
        int currentScore = 0;

        while (elapsedTime < scoreIncreaseDuration)
        {
            currentScore = (int)Mathf.Lerp(0, targetScore, elapsedTime / scoreIncreaseDuration);
            scoreText.text = currentScore.ToString();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终分数是精确的目标分数
        scoreText.text = targetScore.ToString();
    }
}
