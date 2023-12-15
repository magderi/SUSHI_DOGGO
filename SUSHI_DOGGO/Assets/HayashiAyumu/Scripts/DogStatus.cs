using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStatus : MonoBehaviour
{
    public float _jumpPower = 300f;
    public float _movePower = 100f;
    //  現在地から次の地点まで動く時間
    public float _moveTimer = 0.5f;

    public float _maxMoveLimit = 5f;
}
