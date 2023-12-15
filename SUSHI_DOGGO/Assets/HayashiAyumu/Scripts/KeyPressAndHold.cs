using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KeyPressAndHold : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;       // Ä¿±E´¼E
    public Image circleEffect;                  // Î§ÈÆ°´¼E­È¦µÄUI Image
    public float holdDuration = 3f;             // °´×¡µÄ×ûÒÌÊ±¼ä£¨ÃE©
    public string nextSceneName = "YourScene";  // ÏÂÒ»¸ö³¡¾°µÄÃû³Æ

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

            // ¸EÂÔ²È¦µÄÌûÏäÁ¿
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            // Èç¹û°´×¡Ê±¼ä³¬¹ıÖ¸¶¨³ÖĞøÊ±¼ä£¬¼ÓÔØÏÂÒ»¸ö³¡¾°
            if (pressDuration >= holdDuration)
            {
                LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ÖØÖÃÔ²È¦µÄÌûÏäÁ¿
        circleEffect.fillAmount = 0f;
    }

    void LoadNextScene()
    {
        // ¼ÓÔØÏÂÒ»¸ö³¡¾°
        SceneManager.LoadScene(nextSceneName);
    }
}
