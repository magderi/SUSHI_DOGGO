using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
    /// <summary>
    /// QÆ‚Ææ“¾
    /// </summary>
    public static SE_Manager Instance { get => _instance; }

    static SE_Manager _instance;

    public AudioClip[] Audio_Clip_SE;

    public AudioSource Audio_Source_SE;


    private void Start()
    {
        _instance = this;

    }
    /// <summary>
    /// SE‚ğ—¬‚·ˆ—
    /// </summary>
    public void Play(int clip)
    {
        Audio_Source_SE.volume = 1;
        Audio_Source_SE.clip = Audio_Clip_SE[clip];
        Audio_Source_SE.Play();
    }

}

