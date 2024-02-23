using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager2 : MonoBehaviour
{

    /// <summary>
    /// SEを管理するスクリプトです
    /// </summary>


    /// <summary>
    /// 参照と取得
    /// </summary>
    public static SE_Manager2 Instance { get => _instance; }

    static SE_Manager2       _instance;

    public AudioClip[]       Audio_Clip_SE;

    public AudioSource       Audio_Source_SE;


    private void Start()
    {
        _instance = this;

    }
    /// <summary>
    /// SEを流す処理
    /// </summary>
    public void Play(int clip)
    {
        Audio_Source_SE.volume = 1;
        Audio_Source_SE.clip = Audio_Clip_SE[clip];
        Audio_Source_SE.Play();
    }

}

