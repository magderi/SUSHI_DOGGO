using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public Image[] imagesToShow; // Array to hold your images
    public Text scoreText;
    public AudioSource bgmAudioSource;
    private int targetScore = 200;
    private float scoreIncreaseDuration = 2f;

    void Start()
    {
        // 隐藏图片和分数文本
        foreach (Image image in imagesToShow)
        {
            image.gameObject.SetActive(false);
        }
        scoreText.gameObject.SetActive(false);
        targetScore = Random.Range(0, 201);

        // 延迟1秒后显示图片0
        Invoke("ShowImage0", 1f);
    }

    void ShowImage0()
    {
        // 显示图片0
        imagesToShow[0].gameObject.SetActive(true);
        imagesToShow[7].gameObject.SetActive(true);

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

        // 延迟1秒后根据分数范围选择要显示的图片
        Invoke("ShowImagesByScore", 1f);
    }

    void ShowImagesByScore()
    {
        // 根据分数范围选择要显示的图片
        if (targetScore < 100)
        {
            // 显示图片1，隐藏图片2和图片3
            imagesToShow[1].gameObject.SetActive(true);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // 隐藏图片1和图片3，显示图片2
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(true);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // 隐藏图片1和图片2，显示图片3
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(true);
        }
        PlayBGM();
    }
    void PlayBGM()
    {
        // 检查是否有AudioSource和音频文件
        if (bgmAudioSource != null && bgmAudioSource.clip != null)
        {
            // 播放BGM
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("请在Inspector面板中分配AudioSource和音频文件！");
        }
        Invoke("ShowImagesByScore1", 1f);
    }
    void ShowImagesByScore1()
    {
        // 根据分数范围选择要显示的图片
        if (targetScore < 100)
        {
            // 显示图片1，隐藏图片2和图片3
            imagesToShow[4].gameObject.SetActive(true);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // 隐藏图片1和图片3，显示图片2
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(true);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // 隐藏图片1和图片2，显示图片3
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(true);
        }
        
    }
}

