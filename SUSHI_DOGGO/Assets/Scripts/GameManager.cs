using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;  // 追加しましょう

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    // 寿司犬の体力
    private int _sushiHp = 100;

    private void Start()
    {
        _textMeshProUGUI.text = _sushiHp.ToString();
    }

    public void SushiDamage()
    {
        _sushiHp -= 5;
        _textMeshProUGUI.text = _sushiHp.ToString();
    }
}