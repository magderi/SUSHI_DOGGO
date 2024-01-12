using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveLimit : MonoBehaviour
{
    [SerializeField]
    private StandMoving salmonMoving;
    [SerializeField]
    private StandMoving tunaMoving;

    private int _salmonNum;
    private int _tunaNum;

    private bool dontMoveSalmon = false;
    private bool dontMoveTuna = false;

    // Start is called before the first frame update
    void Awake()
    {
        _salmonNum = salmonMoving._laneNamber;
        _tunaNum = tunaMoving._laneNamber;
    }

    // Update is called once per frame
    void Update()
    {
        MoveLimit();
    }

    private void MoveLimit()
    {
        _salmonNum = salmonMoving._laneNamber;
        _tunaNum = tunaMoving._laneNamber;

        //  å›Ç¢ÇâzÇµÇƒÇÃà⁄ìÆñhé~
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

        //  ÉTÅ[ÉÇÉìópï«ìÀÇ´î≤ÇØñhé~
        if (_salmonNum <= 0)
            salmonMoving.canLeftMove = false;
        if (_salmonNum >= 5)
            salmonMoving.canRightMove = false;
        //  É}ÉOÉçópï«ìÀÇ´î≤ÇØñhé~
        if (_tunaNum <= 0)
            tunaMoving.canLeftMove = false;
        if (_tunaNum >= 5)
            tunaMoving.canRightMove = false;
    }
}
