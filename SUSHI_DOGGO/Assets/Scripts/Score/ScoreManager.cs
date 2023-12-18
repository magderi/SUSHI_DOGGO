using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class ScoreManager : MonoBehaviour
{
    // スコアテキスト
    [SerializeField]
    private TextMeshProUGUI _textScoreMeshProUGUI;

    
    [SerializeField]
    private DishScore _dishScore;

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
    async void Start()
    {
        Score();

        await UniTask.Delay(TimeSpan.FromSeconds(4));

        ScoreCheck();
    }

    private void Score()
    {
        _textScoreMeshProUGUI.text = _dishScore._scoreInt.ToString();
    }

    private void ScoreCheck()
    {
        if (_dishScore._scoreInt >= 180)
        {
            _highImage.SetActive(true);
            _sImage.SetActive(true);
        }
        else if (_dishScore._scoreInt >= 140)
        {
            _midleImage.SetActive(true);
            _aImage.SetActive(true);
        }
        else if (_dishScore._scoreInt >= 80)
        {
            _midleImage.SetActive(true);
            _bImage.SetActive(true);
        }
        else if (_dishScore._scoreInt >= 0)
        {
            _lowImage.SetActive(true);
            _dImage.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }
}
