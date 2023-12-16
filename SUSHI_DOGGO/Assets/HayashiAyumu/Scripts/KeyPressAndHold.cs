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

    public KeyCode targetKey = KeyCode.A;       // Ŀ��E���E
    public Image circleEffect;                  // Χ�ư���E�Ȧ��UI Image
    public float holdDuration = 3f;             // ��ס������ʱ�䣨ÁE�
    public string nextSceneName = "YourScene";  // ��һ������������

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

            // ��E�ԲȦ��������
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            // �����סʱ�䳬��ָ������ʱ�䣬������һ������
            if (pressDuration >= holdDuration)
            {
                _seManager.Play(0);

                _maguroAnim.SetTrigger("WakeUp");

                _salmonAnim.SetTrigger("WakeUp");

                _Uicanvs.SetActive(false);

                
               // await UniTask.Delay(TimeSpan.FromSeconds(8));

              //  _fadeManager.fadeout = true;

               // StartCoroutine( _bgmManager.fadeVolue());
         
                //LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ����ԲȦ��������
        circleEffect.fillAmount = 0f;
    }

    public void LoadNextScene()
    {
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }
}
