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
        // ����ͼƬ�ͷ����ı�
        foreach (Image image in imagesToShow)
        {
            image.gameObject.SetActive(false);
        }
        scoreText.gameObject.SetActive(false);
        targetScore = Random.Range(0, 201);

        // �ӳ�1ÁE���ʾͼƬ0
        Invoke("ShowImage0", 1f);
    }

    void ShowImage0()
    {
        // ��ʾͼƬ0
        imagesToShow[0].gameObject.SetActive(true);
        imagesToShow[7].gameObject.SetActive(true);

        // �ӳ�1ÁE���ʾ�����ı�
        Invoke("ShowScore", 1f);
    }

    void ShowScore()
    {
        // ��ʾ�����ı�
        scoreText.gameObject.SetActive(true);

        // �����������ӵ�Э��
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

        // ȷ�����շ����Ǿ�ȷ��Ŀ��E���
        scoreText.text = targetScore.ToString();

        // �ӳ�1ÁE���ݷ�����Χѡ��Ҫ��ʾ��ͼƬ
        Invoke("ShowImagesByScore", 1f);
    }

    void ShowImagesByScore()
    {
        // ���ݷ�����Χѡ��Ҫ��ʾ��ͼƬ
        if (targetScore < 100)
        {
            // ��ʾͼƬ1������ͼƬ2��ͼƬ3
            imagesToShow[1].gameObject.SetActive(true);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // ����ͼƬ1��ͼƬ3����ʾͼƬ2
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(true);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // ����ͼƬ1��ͼƬ2����ʾͼƬ3
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(true);
        }
        PlayBGM();
    }
    void PlayBGM()
    {
        // ��E��Ƿ���AudioSource����Ƶ�ļ�
        if (bgmAudioSource != null && bgmAudioSource.clip != null)
        {
            // ����BGM
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("ǁE�Inspector����з���AudioSource����Ƶ�ļ���");
        }
        Invoke("ShowImagesByScore1", 1f);
    }
    void ShowImagesByScore1()
    {
        // ���ݷ�����Χѡ��Ҫ��ʾ��ͼƬ
        if (targetScore < 100)
        {
            // ��ʾͼƬ1������ͼƬ2��ͼƬ3
            imagesToShow[4].gameObject.SetActive(true);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // ����ͼƬ1��ͼƬ3����ʾͼƬ2
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(true);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // ����ͼƬ1��ͼƬ2����ʾͼƬ3
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(true);
        }
        
    }
}

