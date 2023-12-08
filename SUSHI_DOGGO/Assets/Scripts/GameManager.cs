using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;  // 追加しましょう
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    // 寿司犬の体力
    private int _sushiHp = 100;

    private bool _callOne = false;

    private void Start()
    {
        _textMeshProUGUI.text = _sushiHp.ToString();
    }

    async public void SushiDamage()
    {
        _sushiHp -= 5;
        _textMeshProUGUI.text = _sushiHp.ToString();

        if (_callOne)
        {
           

            

            _callOne = false;
        }

        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }
}