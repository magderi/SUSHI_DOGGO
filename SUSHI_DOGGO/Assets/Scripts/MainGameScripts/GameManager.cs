using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;  // �ǉ����܂��傤
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textSalmonMeshProUGUI;

    [SerializeField]
    private TextMeshProUGUI _textMaguroMeshProUGUI;

    // �X�R�A�e�L�X�g
    [SerializeField]
    private TextMeshProUGUI _textScoreMeshProUGUI;
    // ���݂�HP
    int currentSalmonHp;

    int currentMaguroHp;
    // Slider������
    public Slider sliderSalmonHp;

    public Slider sliderMaguroHp;

    // �X�R�A
    private int _scoreInt;

    // ���i���̗̑�
    private int _sushiSalmonHp = 100;

    private int _sushiMaguroHp = 100;

    private bool _callOne = false;

    private void Start()
    {
        _textSalmonMeshProUGUI.text = _sushiSalmonHp.ToString();

        _textMaguroMeshProUGUI.text = _sushiMaguroHp.ToString();
        //Slider�𖞃^���ɂ���B
        sliderSalmonHp.value = 100;

        sliderMaguroHp.value = 1;

        //���݂�HP���ő�HP�Ɠ����ɁB
        currentSalmonHp = _sushiSalmonHp;
        Debug.Log("Start currentHp : " + currentSalmonHp);

        currentMaguroHp = _sushiMaguroHp;
        Debug.Log("Start currentHp : " + currentMaguroHp);
    }

    async public void SushiSalmonDamage()
    {

        //���݂�HP����_���[�W������
        currentSalmonHp = currentSalmonHp - 5;
        Debug.Log("After currentHp : " + currentSalmonHp);

        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B
        //int���m�̊���Z�͏����_�ȉ���0�ɂȂ�̂ŁA
        //(float)������float�̕ϐ��Ƃ��ĐU���킹��B
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

        //���݂�HP����_���[�W������
        currentMaguroHp = currentMaguroHp - 5;
        Debug.Log("After currentHp : " + currentMaguroHp);

        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f�B
        //int���m�̊���Z�͏����_�ȉ���0�ɂȂ�̂ŁA
        //(float)������float�̕ϐ��Ƃ��ĐU���킹��B
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

   public void ScorePlus()
   {
        _scoreInt =+ 5;

        _textScoreMeshProUGUI.text = "Dish:" + _scoreInt.ToString();

        Debug.Log("ScorePlus ");
    }
}