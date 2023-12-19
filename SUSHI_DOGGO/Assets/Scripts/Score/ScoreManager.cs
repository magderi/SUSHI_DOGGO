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
            _highImage.SetActive(true);
        }
        else if (GlobalVariables.score >= 140)
        {
            _seManager.Play(1);
            _aImage.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _midleImage.SetActive(true);           
        }
        else if (GlobalVariables.score >= 80)
        {
            _seManager.Play(1);
            _bImage.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _midleImage.SetActive(true);            
        }
        else
        {
            _seManager.Play(1);
            _dImage.SetActive(true);            
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _lowImage.SetActive(true);
        }
    }
}
