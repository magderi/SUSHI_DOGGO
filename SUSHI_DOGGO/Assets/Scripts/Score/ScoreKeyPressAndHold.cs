using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreKeyPressAndHold : MonoBehaviour
{
    // [SerializeField]
    //private Animator _salmonAnim;

    //[SerializeField]
    // private Animator _maguroAnim;

    [SerializeField]
    private GameObject _syouyu1;

    [SerializeField]
    private GameObject _syouyu2;

    [SerializeField]
    private GameObject _syouyu3;

    [SerializeField]
    private GameObject _button;

    [SerializeField]
    private SE_Manager _seManager;

    [SerializeField]
    private SE_Manager2 _seManager2;

    [SerializeField]
    private ScoreManager _scoreManager;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private GameObject _syouyuUI;

    [SerializeField]
    private ParticleSystem _soyHi;

    [SerializeField]
    private ParticleSystem _soyMiddle;

    [SerializeField]
    private ParticleSystem _soyLow;

    [SerializeField]
    private Animator _maguroAnimator;

    [SerializeField]
    private Animator _salmonAnimator;

    public KeyCode targetKey = KeyCode.A;       // Ŀ��E���E
    public Image circleEffect;                  // Χ�ư���E�Ȧ��UI Image
    public float holdDuration = 3f;             // ��ס������ʱ�䣨ÁE�
    public string nextSceneName = "YourScene";  // ��һ������������

    private bool isPressing = false;
    private float pressStartTime;

    private ISPlayerMove ISPlayerMove;

    private void Start()
    {
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();
    }

    void Update()
    {
        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            StartPress();
        }

        ContinuePress();


        if (ISPlayerMove.UI.GameStart.WasReleasedThisFrame())
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
                _seManager.Play(2);

               // _maguroAnim.SetTrigger("WakeUp");

                //_salmonAnim.SetTrigger("WakeUp");

                _button.SetActive(false);


                SoysauceDog();

                //  _fadeManager.fadeout = true;

                // StartCoroutine( _bgmManager.fadeVolue());

              
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ����ԲȦ��������
        circleEffect.fillAmount = 0f;
    }

    async public void LoadNextSceneShort()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }

    async public void LoadNextSceneLong()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3));
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }

    async public void SoysauceDog()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));

        // UI非表示
        _canvas.enabled = false;

        // 醤油UI表示
        _syouyuUI.SetActive(true);

        _seManager.Play(3);

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        _seManager.Play(4);

        if (_scoreManager._boolsoyHi)
        {
            _soyHi.Play();          
            await UniTask.Delay(TimeSpan.FromSeconds(1.2));
            _seManager.Play(5);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _seManager2.Play(1);
            _maguroAnimator.SetTrigger("Joy");
            _salmonAnimator.SetTrigger("Joy");
            _syouyu1.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _syouyu2.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _syouyu3.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _maguroAnimator.SetTrigger("Run");
            _salmonAnimator.SetTrigger("Run");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            LoadNextSceneLong();
        }
        else if (_scoreManager._boolsoyMiddle)
        {
            _soyMiddle.Play();         
            await UniTask.Delay(TimeSpan.FromSeconds(1.2));
            _seManager.Play(6);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _seManager2.Play(1);
            _maguroAnimator.SetTrigger("Joy");
            _salmonAnimator.SetTrigger("Joy");
            _syouyu2.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            _syouyu3.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _maguroAnimator.SetTrigger("Run");
            _salmonAnimator.SetTrigger("Run");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            LoadNextSceneLong();
        }
        else
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.6));
            _soyLow.Play();          
            await UniTask.Delay(TimeSpan.FromSeconds(0.6));
            _seManager.Play(7);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            _seManager2.Play(2);
            _syouyu3.SetActive(false);
            _maguroAnimator.SetTrigger("Sad");
            _salmonAnimator.SetTrigger("Sad");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
          
          //  _maguroAnimator.SetTrigger("Run");
         //   _salmonAnimator.SetTrigger("Run");
          //  await UniTask.Delay(TimeSpan.FromSeconds(3));
            LoadNextSceneShort();
        }


    }
}
