using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //  今のところ使っていない
    private DogMoving dogMoving;

    private void Start()
    {
        dogMoving = GetComponent<DogMoving>();
    }


}
