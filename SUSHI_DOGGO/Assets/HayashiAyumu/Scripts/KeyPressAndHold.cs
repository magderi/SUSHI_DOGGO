using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyPressAndHold : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;       // 目眮E醇丒
    public Image circleEffect;                  // 围绕按紒EΦ腢I Image
    public float holdDuration = 3f;             // 按住的姨时间（脕E�
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

            // 竵E略踩Φ奶淞�
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

        // 重置圆圈的帖箱量
        circleEffect.fillAmount = 0f;
    }

    void LoadNextScene()
    {
        // 加载下一个场景
        SceneManager.LoadScene(nextSceneName);
    }
}
