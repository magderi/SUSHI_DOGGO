using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //  ���̂Ƃ���g���Ă��Ȃ�
    private DogMoving dogMoving;

    private void Start()
    {
        dogMoving = GetComponent<DogMoving>();
    }


}
