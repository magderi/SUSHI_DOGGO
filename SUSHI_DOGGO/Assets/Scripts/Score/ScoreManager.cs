using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using static DishScore;

public class ScoreManager : MonoBehaviour
{
    // スコアテキスト
    [SerializeField]
    private TextMeshProUGUI _textScoreMeshProUGUI;

    
    [SerializeField]
    private SE_Manager _seManager;

    // ここを画像の配列にしたい

    [SerializeField]
    private GameObject _sImage;

    [SerializeField]
    private GameObject _aImage;

    [SerializeField]
    private GameObject _bImage;

    [SerializeField]
    private GameObject _dImage;

    [SerializeField]
    private GameObject _highImage;

    [SerializeField]
    private GameObject _midleImage;

    [SerializeField]
    private GameObject _lowImage;

    [SerializeField]
    private GameObject _buttonImage;

    [SerializeField]
    private GameObject _syouyu1;

    [SerializeField]
    private GameObject _syouyu2;

    [SerializeField]
    private GameObject _syouyu3;

    public bool _boolsoyHi;

    public bool _boolsoyMiddle;

    public bool _boolsoyLow;

  

    // Start is called before the first frame update
    void Start()
   {
        Score();
     
   }

    async private void Score()
    {
        _seManager.Play(0);

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        _textScoreMeshProUGUI.text = GlobalVariables.score.ToString();

        await UniTask.Delay(TimeSpan.FromSeconds(2));

        ScoreCheck();    
    }

    async private void ScoreCheck()
    {       
        if (GlobalVariables.score >= 180)
        {
            _seManager.Play(1);
            _sImage.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _syouyu1.SetActive(true);
            _syouyu2.SetActive(true);
            _syouyu3.SetActive(true);
            _highImage.SetActive(true);
            _buttonImage.SetActive(true);
            _boolsoyHi = true;
        }
        else if (GlobalVariables.score >= 140)
        {       
            _seManager.Play(1);
            _aImage.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _syouyu2.SetActive(true);
            _syouyu3.SetActive(true);
            _midleImage.SetActive(true);
            _buttonImage.SetActive(true);
            _boolsoyMiddle = true;
        }
        else if (GlobalVariables.score >= 80)
        {
            _seManager.Play(1);
            _bImage.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _syouyu2.SetActive(true);
            _syouyu3.SetActive(true);
            _midleImage.SetActive(true);
            _buttonImage.SetActive(true);
            _boolsoyMiddle = true;
        }
        else
        {      
            _seManager.Play(1);
            _dImage.SetActive(true);            
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _syouyu3.SetActive(true);
            _lowImage.SetActive(true);
            _buttonImage.SetActive(true);
            _boolsoyLow = true;
        }
    }
}
