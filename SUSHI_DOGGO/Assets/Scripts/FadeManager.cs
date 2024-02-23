using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    /// <summary>
    /// �t�F�[�h�C���E�A�E�g�@�\�̓v�����i�[�̗v���ɂ�荡��͎g�p���Ă��܂���
    /// </summary>

    public GameObject Panelfade;   //�t�F�[�h�p�l���̎擾

    Image fadealpha;               //�t�F�[�h�p�l���̃C���[�W�擾�ϐ�

    private float alpha;           //�p�l����alpha�l�擾�ϐ�

    public bool  fadeout;          //�t�F�[�h�A�E�g�̃t���O�ϐ�
    public bool  fadein;      �@�@ //�t�F�[�h�C���̃t���O�ϐ�



    // Use this for initialization
    void Start()
    {

        fadealpha = Panelfade.GetComponent<Image>(); //�p�l���̃C���[�W�擾
        alpha = fadealpha.color.a;                   //�p�l����alpha�l���擾
        fadein = true;                               //�V�[���ǂݍ��ݎ��Ƀt�F�[�h�C��������
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