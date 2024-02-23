using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCameraJump : MonoBehaviour
{
    /// <summary>
    /// �S�[���W�����v�̎��i����ǂ�������J�����̃X�N���v�g�ł�
    /// </summary>

    public Transform    target;      // �ڕW�n�_��Transform
    public float        height = 5f; // �������̍���

    private float       startTime;
    private float       journeyLength;
    private Vector3     startPos;

    public float        speed = 2.0f; // �ړ����x

    void Start()
    {
        // �����ʒu�̐ݒ�
        startPos = transform.position;

        // �ړ��̊J�n����
        startTime = Time.time;

        // �����ʒu����ڕW�n�_�܂ł̋���
        journeyLength = Vector3.Distance(startPos, target.position);
    }

    void Update()
    {
        Jump();
    }

    public void Jump()
    {
        // ���݂̌o�ߎ���
        float distCovered = (Time.time - startTime) * speed;

        // �i�����i0����1�͈̔́j
        float fracJourney = distCovered / journeyLength;

        // �������̌v�Z
        Vector3 currentPos = Vector3.Lerp(startPos, target.position, fracJourney);
        currentPos.y += Mathf.Sin(fracJourney * Mathf.PI) * height;

        // �I�u�W�F�N�g�̈ړ�
        transform.position = currentPos;

        // �ڕW�n�_�ɓ��B������X�N���v�g�𖳌��ɂ���
        if (fracJourney >= 1.0f)
        {
            enabled = false;
        }
    }
}
