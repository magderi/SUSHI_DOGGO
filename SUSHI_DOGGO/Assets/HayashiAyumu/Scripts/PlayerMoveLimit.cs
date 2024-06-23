using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  寿司犬が本来いけない場所に行かせないようにするスクリプト
public class PlayerMoveLimit : MonoBehaviour
{
    [SerializeField]
    private StandMoving salmonMoving;
    [SerializeField]
    private StandMoving tunaMoving;

    private int _salmonNum;
    private int _tunaNum;

    void Awake()
    {
        _salmonNum = salmonMoving.laneNamber;
        _tunaNum = tunaMoving.laneNamber;
    }

    void Update()
    {
        MoveLimit();
    }

    private void MoveLimit()
    {
        _salmonNum = salmonMoving.laneNamber;
        _tunaNum = tunaMoving.laneNamber;

        //  寿司犬同士がお互いを越さないようにする
        if (_salmonNum >= _tunaNum - 1)
        {
            salmonMoving.canRightMove = false;
            tunaMoving.canLeftMove = false;
        }
        else
        {
            salmonMoving.canRightMove = true;
            tunaMoving.canLeftMove = true;
        }

        //  寿司犬が壁を越えていかないようにする
        if (_salmonNum <= 0)
            salmonMoving.canLeftMove = false;
        if (_salmonNum >= 5)
            salmonMoving.canRightMove = false;
        if (_tunaNum <= 0)
            tunaMoving.canLeftMove = false;
        if (_tunaNum >= 5)
            tunaMoving.canRightMove = false;
    }
}
