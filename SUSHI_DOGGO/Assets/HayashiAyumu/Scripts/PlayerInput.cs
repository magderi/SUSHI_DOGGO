using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //  ç°ÇÃÇ∆Ç±ÇÎégÇ¡ÇƒÇ¢Ç»Ç¢
    private DogMoving dogMoving;

    private void Start()
    {
        dogMoving = GetComponent<DogMoving>();
    }


}
