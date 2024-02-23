using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{

    /// <summary>
    /// 寿司犬がゴールした際に起こすフラグを、まとめたスクリプトです
    /// </summary>

    // 以下参照
    [SerializeField]
    private GameObject      _goalText;

    [SerializeField]
    private SE_Manager      _seManager;

    [SerializeField]
    private BGM_Manager     _bgmManager;

    [SerializeField]
    private FadeManager     _fadeManager;

    [SerializeField]
    private Animator        _maguroanimator;

    [SerializeField]
    private Animator        _salmonanimator;
    // Start is called before the first frame update
    void Start()
    {
        _bgmManager.Play(0);
    }

     
    // ゴール到着時に起動する処理
    async private void OnTriggerEnter(Collider other)
    {
        _salmonanimator.SetBool("GoalJump", false);
        _maguroanimator.SetBool("GoalJump", false);

        _goalText.SetActive(true);

        _seManager.Play(1);

        await UniTask.Delay(TimeSpan.FromSeconds(3));

        SceneManager.LoadScene("score");
    }
}
