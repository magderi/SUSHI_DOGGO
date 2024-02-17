using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    //  InputSystem取得
    private ISPlayerMove ISPlayerMove;

    //  チュートリアルのキャンバス関連
    [SerializeField]
    private GameObject tutorialCanvas;      //  チュートリアルのキャンバス
    [SerializeField]
    private Sprite Tutorial1;               //  チュートリアル1のSprite
    [SerializeField]
    private Sprite Tutorial2;               //  チュートリアル2のSprite
    [SerializeField]
    private Image tutorialImage;            //  Spriteを入れるImage


    //  ゲーム開始直前のカウントダウンテキスト関連
    [SerializeField]
    private TMP_Text StartCountdownText;    //  カウントダウンのテキスト
    [SerializeField]
    private GameObject StartTextGameObject; //  カウントダウンテキストのゲームオブジェクト
    

    //  寿司犬たちの格納場所
    [SerializeField]
    private GameObject salmon;
    [SerializeField]
    private GameObject maguro;

    //  寿司犬の移動スクリプト
    private StandMoving maguroStandMoving;
    private StandMoving salmonStandMoving;


    //  チュートリアルを進められるかどうか
    private bool canTutorialNext = true;
    //  カウントダウン時間
    private int countdownSeconds = 3;

    // Start is called before the first frame update
    void Start()
    {
        //  InputSystemを有効化
        ISPlayerMove = new ISPlayerMove();
        ISPlayerMove.Enable();
        //  ゲームを時間停止
        Time.timeScale = 0f;


        //  チュートリアルを表示
        bool isActive = tutorialCanvas.activeSelf;
        if(!isActive)
            tutorialCanvas.SetActive(true);
        tutorialImage.sprite = Tutorial1;
        tutorialImage.color = Color.white;


        //  寿司犬たちの操作を無効化
        salmonStandMoving = salmon.GetComponent<StandMoving>();
        maguroStandMoving = maguro.GetComponent<StandMoving>();
        salmonStandMoving.enabled = false;
        maguroStandMoving.enabled = false;


        //  開始一秒は操作が効かないように
        StartCoroutine(WaitNextCor());
    }

    // Update is called once per frame
    void Update()
    {
        TutorialNext();
    }

    /// <summary>
    /// チュートリアル中の操作受付処理
    /// </summary>
    private void TutorialNext()
    {
        //  主にコルーチンを使っていれば操作を受け付けない
        if (!canTutorialNext)   return;

        //  進めるボタンを押したとき
        if (ISPlayerMove.UI.GameStart.WasPressedThisFrame())
        {
            //  チュートリアルが一枚目の時
            if (tutorialImage.sprite == Tutorial1)
            {
                //  チュートリアルを次の絵へ
                tutorialImage.sprite = Tutorial2;
                //  次の処理まで一秒待機
                StartCoroutine(WaitNextCor());
            }
            //  チュートリアルが二枚目の時
            else if (tutorialImage.sprite == Tutorial2)
            {
                StartCoroutine(WaitStartCor());
            }
        }
        //  スキップボタンを押したとき
        else if (ISPlayerMove.UI.Skip.WasPressedThisFrame())
        {
            StartCoroutine(WaitStartCor());
        }
    }

    /// <summary>
    /// 一秒後に操作可能にするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitNextCor()
    {
        canTutorialNext = false;
        //  TimeScaleに影響されない１秒待機
        yield return new WaitForSecondsRealtime(1.0f);
        canTutorialNext = true;
    }

    /// <summary>
    /// スタート時の開始カウントダウン
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStartCor()
    {
        //  TutorialCanvasを無効化
        tutorialCanvas.SetActive(false);

        //  カウントダウンが終わるまで繰り返し
        for (int i = countdownSeconds; i >= 0; i--)
        {
            //  カウントダウンをTextとして表示
            if (i == 0)
            {
                StartCountdownText.SetText("GO");
            }
            else
            {
                string str = i.ToString();
                StartCountdownText.SetText(str);
            }
            //  1秒ごとに繰り返すように
            yield return new WaitForSecondsRealtime(1.0f);
        }

        //  寿司犬たちの操作を有効化
        salmonStandMoving.enabled = true;
        maguroStandMoving.enabled = true;
        //  ゲームの時間を進める
        Time.timeScale = 1f;
        //  テキストのオブジェクトを無効化
        StartTextGameObject.SetActive(false);
    }
}
