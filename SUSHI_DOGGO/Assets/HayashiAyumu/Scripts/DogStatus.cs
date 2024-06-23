using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  寿司犬達の移動に使う数値の入れ物
public class DogStatus : MonoBehaviour
{
    public float _jumpPower = 300f;
    public float _movePower = 100f;

    public float _moveTimer = 0.2f;

    public float _maxMoveLimit = 5f;
}
