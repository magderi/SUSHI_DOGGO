using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class KeyPressAndHold : MonoBehaviour
{
    [SerializeField]
    private Animator _salmonAnim;

    [SerializeField]
    private Animator _maguroAnim;

    [SerializeField]
    private FadeManager _fadeManager;

    [SerializeField]
    private GameObject _Uicanvs;

    [SerializeField]
    private SE_Manager _seManager;

    [SerializeField]
    private BGM_Manager _bgmManager;

    public KeyCode targetKey = KeyCode.A;       // ƒø±ÅE¥ºÅE
    public Image circleEffect;                  // Œß»∆∞¥ºÅE≠»¶µƒUI Image
    public float holdDuration = 3f;             // ∞¥◊°µƒ◊˚“Ã ±º‰£®√ÅE©
    public string nextSceneName = "YourScene";  // œ¬“ª∏ˆ≥°æ∞µƒ√˚≥∆

    private bool isPressing = false;
    private float pressStartTime;

    private void Start()
    {
        _bgmManager.Play(0);
    }

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

    async void ContinuePress()
    {
        if (isPressing)
        {
            _fadeManager.fadein = false;

            float pressDuration = Time.time - pressStartTime;

            // ∏ÅE¬‘≤»¶µƒÃ˚œ‰¡ø
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            // »Áπ˚∞¥◊° ±º‰≥¨π˝÷∏∂®≥÷–¯ ±º‰£¨º”‘ÿœ¬“ª∏ˆ≥°æ∞
            if (pressDuration >= holdDuration)
            {
                _seManager.Play(0);

                _maguroAnim.SetTrigger("WakeUp");

                _salmonAnim.SetTrigger("WakeUp");

                _Uicanvs.SetActive(false);

                
                await UniTask.Delay(TimeSpan.FromSeconds(9));

                _fadeManager.fadeout = true;

                StartCoroutine( _bgmManager.fadeVolue());

                await UniTask.Delay(TimeSpan.FromSeconds(3));

                LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ÷ÿ÷√‘≤»¶µƒÃ˚œ‰¡ø
        circleEffect.fillAmount = 0f;
    }

    void LoadNextScene()
    {
        // º”‘ÿœ¬“ª∏ˆ≥°æ∞
        SceneManager.LoadScene(nextSceneName);
    }
}
