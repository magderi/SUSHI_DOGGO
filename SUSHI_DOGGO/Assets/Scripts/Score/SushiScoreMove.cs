using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScoreMove : MonoBehaviour
{
    //��]�����ǂ���
    bool coroutineBool = false;

    public bool _spinStart = false;

    void Update()
    {
        /*
        if (_spinStart == true)
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("RightMove");
            }
        }
        */

        if (_spinStart == true)
        {
            //��]���ł͂Ȃ��ꍇ�͎��s 
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("LeftMove");
            }
        }
    }

    //�E�ɂ�������]����90���ŃX�g�b�v
    IEnumerator RightMove()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
    }

    //���ɂ�������]����90���ŃX�g�b�v
    IEnumerator LeftMove()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
        //���W������������
        transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
    }

    public void MoveLeft()
    {
        _spinStart = true;
    }
}
