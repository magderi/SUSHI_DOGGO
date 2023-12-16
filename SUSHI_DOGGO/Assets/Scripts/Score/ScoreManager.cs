using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // �X�R�A�e�L�X�g
    [SerializeField]
    private TextMeshProUGUI _textScoreMeshProUGUI;

    [SerializeField]
    private DishScore _dishScore;
    // Start is called before the first frame update
    void Start()
    {
        Score();
    }

    private void Score()
    {
        _textScoreMeshProUGUI.text = _dishScore._scoreInt.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Score();
    }
}
