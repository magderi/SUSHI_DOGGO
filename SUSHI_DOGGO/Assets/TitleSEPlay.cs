using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSEPlay : MonoBehaviour
{

    public SE_Manager _seManager;

    public void MoveSEPlay()
    {
        _seManager.Play(1);
    }
  
}
