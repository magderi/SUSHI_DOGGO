using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DishScore : MonoBehaviour
{
    // �X�R�A
    public static int score;

    public int _scoreInt;
    private void Update()
    {
        // �v���C���[�̑���ȂǂŃX�R�A���ς��ꍇ�A�����ŏ�������
        // ��: _scoreInt���X�R�A��\���ꍇ
        score = _scoreInt;
    }

    private void OnDestroy()
    {
        // �V�[�����j������鎞�ɃX�R�A��ۑ�
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
}

