using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0f, 0f, 0f);  // Z軸を10°回転
    }
}
