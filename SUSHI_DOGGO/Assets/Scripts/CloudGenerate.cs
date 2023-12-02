using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerate : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int tateNumber; // �s���ɑ���
    private int yokoNumber; // �񐔂ɑ���

    /*
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject ballPrefab;
    */
    [SerializeField]
    private GameObject coinPrefab;

    private void Start()
    {
        string textLines = textFile.text; // �e�L�X�g�̑S�̃f�[�^�̑��
        print(textLines);

        // ���s�Ńf�[�^�𕪊����Ĕz��ɑ��
        textData = textLines.Split('\n');

        // �s���Ɨ񐔂̎擾
        yokoNumber = textData[0].Split(',').Length;
        tateNumber = textData.Length;

        print("tate" + tateNumber);
        print("yoko" + yokoNumber);

        // �Q�����z��̒�`
        dungeonMap = new string[tateNumber, yokoNumber];

        for (int i = 0; i < tateNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');

            for (int j = 0; j < yokoNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];

                if (dungeonMap[i, j] != null)
                {
                    switch (dungeonMap[i, j])
                    {
                        /*
                        case "w":
                            Instantiate(wallPrefab, new Vector3(-4.5f + j, 0.5f, 4.5f - i), Quaternion.identity);
                            break;

                        case "b":
                            Instantiate(ballPrefab, new Vector3(-4.5f + j, 0.5f, 4.5f - i), Quaternion.identity);
                            break;
                        */
                        case "c":
                            Instantiate(coinPrefab, new Vector3(-2.5f + j, 2.5f, 60f - i), Quaternion.Euler(0, 0, 0));
                            break;
                    }
                }
            }
        }
    }
}
