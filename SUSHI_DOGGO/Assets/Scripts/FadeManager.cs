using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    /// <summary>
    /// フェードイン・アウト機能はプランナーの要請により今回は使用していません
    /// </summary>

    public GameObject Panelfade;   //フェードパネルの取得

    Image fadealpha;               //フェードパネルのイメージ取得変数

    private float alpha;           //パネルのalpha値取得変数

    public bool  fadeout;          //フェードアウトのフラグ変数
    public bool  fadein;      　　 //フェードインのフラグ変数



    // Use this for initialization
    void Start()
    {

        fadealpha = Panelfade.GetComponent<Image>(); //パネルのイメージ取得
        alpha = fadealpha.color.a;                   //パネルのalpha値を取得
        fadein = true;                               //シーン読み込み時にフェードインさせる
    }

    // Update is called once per frame
    void Update()
    {

        if (fadein == true)
        {
            FadeIn();
        }

        if (fadeout == true)
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        alpha -= 0.005f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha <= 0)
        {
            fadein = false;          
        }
    }

    void FadeOut()
    {
        alpha += 0.005f;
        fadealpha.color = new Color(0, 0, 0, alpha);
        if (alpha >= 1)
        {
            fadeout = false;
        }
    }

    public void SceneMove()
    {
        fadeout = true;
    }
}