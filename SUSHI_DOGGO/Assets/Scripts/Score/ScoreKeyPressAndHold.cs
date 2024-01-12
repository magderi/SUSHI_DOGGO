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
    private GameObject _Uicanvs;

    [SerializeField]
    private SE_Manager _seManager;

  

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

                _Uicanvs.SetActive(false);


                 

                //  _fadeManager.fadeout = true;

                // StartCoroutine( _bgmManager.fadeVolue());

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

    async public void LoadNextScene()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5));
        // ������һ������
        SceneManager.LoadScene(nextSceneName);
    }
}
