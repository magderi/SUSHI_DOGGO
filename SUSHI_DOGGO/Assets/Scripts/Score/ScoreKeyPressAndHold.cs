﻿using Cysharp.Threading.Tasks;
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

              //  LoadNextScene();
            }
        }
    }

    void EndPress()
    {
        isPressing = false;

        // ����ԲȦ��������
        circleEffect.fillAmount = 0f;
    }

    async public void LoadNextScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }    

    async public void SoysauceDog()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));

        // UI非表示
        _canvas.enabled = false;

        _seManager.Play(3);

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        _seManager.Play(4);

        if (_scoreManager._boolsoyHi)
        {
            _seManager.Play(5);
            _seManager2.Play(0);
            _soyHi.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            _maguroAnimator.SetTrigger("Joy");
            _salmonAnimator.SetTrigger("Joy");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            _maguroAnimator.SetTrigger("Run");
            _salmonAnimator.SetTrigger("Run");
        }
        else if (_scoreManager._boolsoyMiddle)
        {
            _seManager.Play(6);
            _soyMiddle.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            _maguroAnimator.SetTrigger("Joy");
            _salmonAnimator.SetTrigger("Joy");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            _maguroAnimator.SetTrigger("Run");
            _salmonAnimator.SetTrigger("Run");
        }
        else
        {
            _seManager.Play(7);
            _soyLow.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5));
            _maguroAnimator.SetTrigger("Sad");
            _salmonAnimator.SetTrigger("Sad");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            _maguroAnimator.SetTrigger("Run");
            _salmonAnimator.SetTrigger("Run");
        }
        
    
    }
}
