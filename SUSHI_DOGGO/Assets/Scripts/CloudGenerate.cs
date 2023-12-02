using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGenerate : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile;

    private string[] textData;
    private string[,] dungeonMap;

    private int tateNumber; // 行数に相当
    private int yokoNumber; // 列数に相当

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
        string textLines = textFile.text; // テキストの全体データの代入
        print(textLines);

        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');

        // 行数と列数の取得
        yokoNumber = textData[0].Split(',').Length;
        tateNumber = textData.Length;

        print("tate" + tateNumber);
        print("yoko" + yokoNumber);

        // ２次元配列の定義
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
