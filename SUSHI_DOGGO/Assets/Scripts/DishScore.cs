using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DishScore : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    public static class GlobalVariables
    {
        public static int score;
    }

    private void Update()
    {
        GlobalVariables.score = gameManager.score;

        // �v���C���[�̑���ȂǂŃX�R�A���ς��ꍇ�A�����ŏ�������
        // ��: _scoreInt���X�R�A��\���ꍇ
        //score ;
    }

}

