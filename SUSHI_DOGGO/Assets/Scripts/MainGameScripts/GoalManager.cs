using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _goalText;


    [SerializeField]
    private SE_Manager _seManager;

    [SerializeField]
    private BGM_Manager _bgmManager;

    [SerializeField]
    private FadeManager _fadeManager;

    [SerializeField]
    private Animator _maguroanimator;

    [SerializeField]
    private Animator _salmonanimator;
    // Start is called before the first frame update
    void Start()
    {
        _bgmManager.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
