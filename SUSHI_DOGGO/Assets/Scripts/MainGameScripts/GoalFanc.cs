using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFanc : MonoBehaviour
{
    /// <summary>
    /// �S�[����O�ɂȂ�����N������X�N���v�g�ł�
    /// </summary>


    // �ȉ����ꂼ��Q��
    [SerializeField]
    private GoalJump    _goalJump;

    [SerializeField]
    private SushiJump   _shushiJump;

    [SerializeField]
    private StandMoving _standMoving;

    // �S�[�����C���ɐڐG���ɋN��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalLine"))
        {
            _goalJump.enabled = true;

            _shushiJump.enabled = false;

            _standMoving.enabled = false;
        }
    }
}
