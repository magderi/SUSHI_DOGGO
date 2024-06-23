using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

//  シーン切り替えボタンを長押しした際のスクリプト
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

    public Image circleEffect;                  //  進むための輪っか状の画像
    public float holdDuration = 3f;             //  シーン遷移するのに必要な時間
    public string nextSceneName = "";  // 次のシーン名を入れる箱

    private bool isPressing = false;
    private float pressStartTime;

    private ISPlayerMove ISPlayerMove;

    private void Start()
    {
        _bgmManager.Play(0);

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

    public void SEPlay()
    {
        _seManager.Play(0);
    }
    void ContinuePress()
    {
        if (isPressing)
        {
            _fadeManager.fadein = false;

            float pressDuration = Time.time - pressStartTime;

            // ボタンを押した時間に伴って輪っかを満たしていく
            float fillAmount = Mathf.Clamp01(pressDuration / holdDuration);
            circleEffect.fillAmount = fillAmount;

            //  ボタンを押した時間が必要な時間を超えたら
            if (pressDuration >= holdDuration)
            {

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

    /// <summary>
    /// 長押しをやめたときの関数
    /// </summary>
    void EndPress()
    {
        isPressing = false;
        circleEffect.fillAmount = 0f;
    }

    //  次のシーンに移行するときに使う関数
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
