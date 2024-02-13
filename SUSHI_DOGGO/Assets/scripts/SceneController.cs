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
        // Òş²ØÍ¼Æ¬ºÍ·ÖÊıÎÄ±¾
        foreach (Image image in imagesToShow)
        {
            image.gameObject.SetActive(false);
        }
        scoreText.gameObject.SetActive(false);
        targetScore = Random.Range(0, 201);

        // ÑÓ³Ù1ÃEóÏÔÊ¾Í¼Æ¬0
        Invoke("ShowImage0", 1f);
    }

    void ShowImage0()
    {
        // ÏÔÊ¾Í¼Æ¬0
        imagesToShow[0].gameObject.SetActive(true);
        imagesToShow[7].gameObject.SetActive(true);

        // ÑÓ³Ù1ÃEóÏÔÊ¾·ÖÊıÎÄ±¾
        Invoke("ShowScore", 1f);
    }

    void ShowScore()
    {
        // ÏÔÊ¾·ÖÊıÎÄ±¾
        scoreText.gameObject.SetActive(true);

        // Æô¶¯·ÖÊıÔö¼ÓµÄĞ­³Ì
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

        // È·±£×ûòÕ·ÖÊıÊÇ¾«È·µÄÄ¿±EÖÊı
        scoreText.text = targetScore.ToString();

        // ÑÓ³Ù1ÃEó¸ù¾İ·ÖÊı·¶Î§Ñ¡ÔñÒªÏÔÊ¾µÄÍ¼Æ¬
        Invoke("ShowImagesByScore", 1f);
    }

    void ShowImagesByScore()
    {
        // ¸ù¾İ·ÖÊı·¶Î§Ñ¡ÔñÒªÏÔÊ¾µÄÍ¼Æ¬
        if (targetScore < 100)
        {
            // ÏÔÊ¾Í¼Æ¬1£¬Òş²ØÍ¼Æ¬2ºÍÍ¼Æ¬3
            imagesToShow[1].gameObject.SetActive(true);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // Òş²ØÍ¼Æ¬1ºÍÍ¼Æ¬3£¬ÏÔÊ¾Í¼Æ¬2
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(true);
            imagesToShow[3].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // Òş²ØÍ¼Æ¬1ºÍÍ¼Æ¬2£¬ÏÔÊ¾Í¼Æ¬3
            imagesToShow[1].gameObject.SetActive(false);
            imagesToShow[2].gameObject.SetActive(false);
            imagesToShow[3].gameObject.SetActive(true);
        }
        PlayBGM();
    }
    void PlayBGM()
    {
        // ¼EéÊÇ·ñÓĞAudioSourceºÍÒôÆµÎÄ¼ş
        if (bgmAudioSource != null && bgmAudioSource.clip != null)
        {
            // ²¥·ÅBGM
            bgmAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("ÇEÚInspectorÃæ°åÖĞ·ÖÅäAudioSourceºÍÒôÆµÎÄ¼ş£¡");
        }
        Invoke("ShowImagesByScore1", 1f);
    }
    void ShowImagesByScore1()
    {
        // ¸ù¾İ·ÖÊı·¶Î§Ñ¡ÔñÒªÏÔÊ¾µÄÍ¼Æ¬
        if (targetScore < 100)
        {
            // ÏÔÊ¾Í¼Æ¬1£¬Òş²ØÍ¼Æ¬2ºÍÍ¼Æ¬3
            imagesToShow[4].gameObject.SetActive(true);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 100 && targetScore < 150)
        {
            // Òş²ØÍ¼Æ¬1ºÍÍ¼Æ¬3£¬ÏÔÊ¾Í¼Æ¬2
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(true);
            imagesToShow[6].gameObject.SetActive(false);
        }
        else if (targetScore >= 150)
        {
            // Òş²ØÍ¼Æ¬1ºÍÍ¼Æ¬2£¬ÏÔÊ¾Í¼Æ¬3
            imagesToShow[4].gameObject.SetActive(false);
            imagesToShow[5].gameObject.SetActive(false);
            imagesToShow[6].gameObject.SetActive(true);
        }
        
    }
}

