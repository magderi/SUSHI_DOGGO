using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class DishScore : MonoBehaviour
{
    /// <summary>
    /// ���C���Q�[����Dish�̒l���Ǘ�����X�N���v�g�ł�
    /// </summary>

    [SerializeField]
    private GameManager gameManager;

    public static class GlobalVariables
    {
        // �����̃X�R�A�̒l��ύX����ƃ��U���g�V�[���ł̃f�o�b�N���o���܂��B�i�� score = 100�@���j
        public static int score;
    }

    private void Update()
    {
        GlobalVariables.score = gameManager.score;
    }
}

