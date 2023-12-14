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
    private TextMeshProUGUI _textSalmonMeshProUGUI;

    [SerializeField]
    private TextMeshProUGUI _textMaguroMeshProUGUI;
    // 現在のHP
    int currentSalmonHp;

    int currentMaguroHp;
    // Sliderを入れる
    public Slider sliderSalmonHp;

    public Slider sliderMaguroHp;

    // 寿司犬の体力
    private int _sushiSalmonHp = 100;

    private int _sushiMaguroHp = 100;

    private bool _callOne = false;

    private void Start()
    {
        _textSalmonMeshProUGUI.text = _sushiSalmonHp.ToString();

        _textMaguroMeshProUGUI.text = _sushiMaguroHp.ToString();
        //Sliderを満タンにする。
        sliderSalmonHp.value = 100;

        sliderMaguroHp.value = 1;

        //現在のHPを最大HPと同じに。
        currentSalmonHp = _sushiSalmonHp;
        Debug.Log("Start currentHp : " + currentSalmonHp);

        currentMaguroHp = _sushiMaguroHp;
        Debug.Log("Start currentHp : " + currentMaguroHp);
    }

    async public void SushiSalmonDamage()
    {

        //現在のHPからダメージを引く
        currentSalmonHp = currentSalmonHp - 5;
        Debug.Log("After currentHp : " + currentSalmonHp);

        //最大HPにおける現在のHPをSliderに反映。
        //int同士の割り算は小数点以下は0になるので、
        //(float)をつけてfloatの変数として振舞わせる。
        sliderSalmonHp.value = currentSalmonHp;
        Debug.Log("slider.value : " + sliderSalmonHp.value);
        _sushiSalmonHp -= 5;
        _textSalmonMeshProUGUI.text = _sushiSalmonHp.ToString();

        if (_callOne)
        {           

            _callOne = false;
        }

        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }


    async public void SushiMaguroDamage()
    {

        //現在のHPからダメージを引く
        currentMaguroHp = currentMaguroHp - 5;
        Debug.Log("After currentHp : " + currentMaguroHp);

        //最大HPにおける現在のHPをSliderに反映。
        //int同士の割り算は小数点以下は0になるので、
        //(float)をつけてfloatの変数として振舞わせる。
        sliderMaguroHp.value = (float)currentMaguroHp / (float)_sushiMaguroHp; ;
        Debug.Log("slider.value : " + sliderMaguroHp.value);
        _sushiMaguroHp -= 5;
        _textMaguroMeshProUGUI.text = _sushiMaguroHp.ToString();

        if (_callOne)
        {

            _callOne = false;
        }

        await UniTask.Delay(TimeSpan.FromSeconds(3));
    }
}