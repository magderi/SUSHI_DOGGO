using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//  チュートリアルを表示するスクリプト
//  展示会1日目と2日目の間に作ったので粗があるかも。
public class TutorialController : MonoBehaviour
{
    private ISPlayerMove ISPlayerMove;
    private TutorialController tutorialController;

    //  チュートリアル画像たちを入れる箱
    [SerializeField]
    private GameObject tutorialCanvas;
    private Sprite Tutorial1;
    [SerializeField]
    private Sprite Tutorial2;
    [SerializeField]
    private Image tutorialImage;


    //  カウントダウンをテキストで表示するための箱
    [SerializeField]
    private TMP_Text StartCountdownText;
    [SerializeField]
    private GameObject StartTextGameObject;
    

    [SerializeField]
    private GameObject salmon;
    [SerializeField]
    private GameObject maguro;

    private StandMoving maguroStandMoving;
    private StandMoving salmonStandMoving;


    private bool canTutorialNext = true;
    private int countdownSeconds = 3;

    void Start()
    {
        //  InputSystemを有効化
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();
        //  一旦移動等の操作を不可能に
        Time.timeScale = 0f;


        //  チュートリアル画像を表示させる
        bool isActive = tutorialCanvas.activeSelf;
        if(!isActive)
            tutorialCanvas.SetActive(true);
        tutorialImage.sprite = Tutorial1;
        tutorialImage.color = Color.white;


        //  寿司犬たちの操作を無効に
        salmonStandMoving = salmon.GetComponent<StandMoving>();
        maguroStandMoving = maguro.GetComponent<StandMoving>();
        salmonStandMoving.enabled = false;
        maguroStandMoving.enabled = false;

        //  開始直後にボタンを押して見逃さないように
        StartCoroutine(WaitNextCor());

        tutorialController = this.GetComponent<TutorialController>();
    }

    void Update()
    {
        TutorialNext();
    }

    /// <summary>
    /// チュートリアルの画像を次の画像に切り替える関数
    /// </summary>
    private void TutorialNext()
    {
        if (!canTutorialNext)   return;

        //  Aボタンを押した際の処理
        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            //  表示されている画像に応じて処理を変える
            if (tutorialImage.sprite == Tutorial1)
            {
                //  次の画像を表示
                tutorialImage.sprite = Tutorial2;
                StartCoroutine(WaitNextCor());
            }
            else if (tutorialImage.sprite == Tutorial2)
            {
                StartCoroutine(WaitStartCor());
            }
        }
        //  スキップボタンが押されたら
        else if (ISPlayerMove.UI.Skip.WasPressedThisFrame())
        {
            StartCoroutine(WaitStartCor());
        }
    }

    /// <summary>
    /// 一秒間は連打しても次のシーンに行かないようにするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitNextCor()
    {
        canTutorialNext = false;
        yield return new WaitForSecondsRealtime(1.0f);
        canTutorialNext = true;
    }

    /// <summary>
    /// スタートするまでのカウントダウンをするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStartCor()
    {
        //  TutorialCanvasを非表示に
        tutorialCanvas.SetActive(false);

        //  カウントダウンの文字を繰り返し更新
        for (int i = countdownSeconds; i >= 0; i--)
        {
            //  開始時間になったら
            if (i == 0)
            {
                StartCountdownText.SetText("GO");
            }
            else
            {
                string str = i.ToString();
                StartCountdownText.SetText(str);
            }
            //  1秒ごとに表示を更新
            yield return new WaitForSecondsRealtime(1.0f);
        }

        //  寿司犬たちの操作を有効に
        salmonStandMoving.enabled = true;
        maguroStandMoving.enabled = true;
        Time.timeScale = 1f;
        //  カウントダウンとチュートリアルの操作を非表示に
        StartTextGameObject.SetActive(false);
        tutorialController.enabled = false;
    }
}
