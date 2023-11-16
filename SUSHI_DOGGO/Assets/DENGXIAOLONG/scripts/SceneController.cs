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
        // ����ͼƬ�ͷ����ı�
        imageToShow.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        targetScore = Random.Range(0, 201);

        // �ӳ�1�����ʾͼƬ
        Invoke("ShowImage", 1f);
    }

    void ShowImage()
    {
        // ��ʾͼƬ
        imageToShow.gameObject.SetActive(true);

        // �ӳ�1�����ʾ�����ı�
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

        // ȷ�����շ����Ǿ�ȷ��Ŀ�����
        scoreText.text = targetScore.ToString();
    }
}
