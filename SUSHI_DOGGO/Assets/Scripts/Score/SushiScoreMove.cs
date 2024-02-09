using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScoreMove : MonoBehaviour
{
    public SE_Manager _se_Manager;

    void Start()
    {
        
    }

  

    void Update()
    {
    
    }



    public void ScoreJoyMoveSE()
    {
        _se_Manager.Play(8);
    }
}
