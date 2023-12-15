using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyPressAndHold : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;       // Ŀ��E���E
    public Image circleEffect;                  // Χ�ư���E�Ȧ��UI Image
    public float holdDuration = 3f;             // ��ס������ʱ�䣨ÁE�
    public string nextSceneName = "YourScene";  // ��һ������������

    private bool isPressing = false;
    private float pressStartTime;

    void Update()
    {
        if (Input.GetKeyDown(targetKey))
        {
            StartPress();
        }

        if (Input.GetKey(targetKey))
        {
            ContinuePress();
        }

        if (Input.GetKeyUp(targetKey))
        {
            EndPress();
        }
    }

    void StartPress()
    {
        isPressing = true;
        pressStartTime = Time.time;
    }

    void ContinuePress()
    {
        if (isPressing)
        {
            float pressDuration = Time.time - pressStartTime;

            // ��E�ԲȦ��������
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            // �����סʱ�䳬��ָ������ʱ�䣬������һ������
            if (pressDuration >= holdDuration)
            {
                LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ����ԲȦ��������
        circleEffect.fillAmount = 0f;
    }

    void LoadNextScene()
    {
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }
}
