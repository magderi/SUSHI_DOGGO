using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyPressAndHold : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;       // 目标按键
    public Image circleEffect;                  // 围绕按键画圈的UI Image
    public float holdDuration = 3f;             // 按住的最短时间（秒）
    public string nextSceneName = "YourScene";  // 下一个场景的名称

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

            // 更新圆圈的填充量
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            // 如果按住时间超过指定持续时间，加载下一个场景
            if (pressDuration >= holdDuration)
            {
                LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // 重置圆圈的填充量
        circleEffect.fillAmount = 0f;
    }

    void LoadNextScene()
    {
        // 加载下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
