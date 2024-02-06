using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiScoreMove : MonoBehaviour
{
    public float targetYRotation = 100f; // �ڕW��y����]�p�x
    public float rotationSpeed = 90f; // ��]���x

    private bool isRotating = false; // ��]�����ǂ����̃t���O
    public bool _scorejoyMove = false;

    public Transform target; // �ڕW�ʒu

    private float speed = 0.5f; // �ړ����x�i�C�ӂ̒l�ɒ������Ă��������j
    private float startTime; // �ړ��J�n����

    void Start()
    {
        
    }

    void RotateToTarget()
    {
        // �I�u�W�F�N�g�̌��݂�Rotation���擾
        Quaternion currentRotation = transform.rotation;

        // �ڕW��Rotation���v�Z
        Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);

        // ��]�̊J�n
        StartCoroutine(RotateCoroutine(currentRotation, targetRotation));
    }


    void Update()
    {
        if (_scorejoyMove == true)
        {
            startTime = Time.time; // �ړ��J�n���Ԃ��L�^

            // �ڕW�ʒu�Ɍ������Ĉړ���������x�N�g�����v�Z
            Vector3 direction = (target.position - transform.position).normalized;

            // �ڕW�ʒu�Ɍ������Ĉ��̑��x�ňړ�
            transform.position += direction * speed * Time.deltaTime;

            // 5�b��Ɉړ����~
            if (Time.time - startTime >= 5f)
            {
                enabled = false; // �X�N���v�g�𖳌��ɂ��邱�Ƃňړ����~
            }
        }
    }

    IEnumerator RotateCoroutine(Quaternion startRotation, Quaternion targetRotation)
    {
      
        // ��]���̃t���O�𗧂Ă�
        isRotating = true;

        // ��]�J�n����
        float startTime = Time.time;

        // ��]����������܂ł̌o�ߎ���
        float journeyLength = Quaternion.Angle(startRotation, targetRotation);

        while (isRotating)
        {
            // ���݂̌o�ߎ���
            float elapsedTime = Time.time - startTime;

            // ��]�̐i�s�x�������v�Z
            float fractionOfJourney = Mathf.Clamp01(elapsedTime * 1f); // 1�b�����ĉ�]����

            // ��]����
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, fractionOfJourney);

            // �ڕW�ɓ��B�������ǂ������`�F�b�N
            if (fractionOfJourney >= 1f)
            {
                // ��]����
                isRotating = false;
               
            }

            // 1�t���[���ҋ@
            yield return null;
        }




    }

    public void ScoreJoyMove()
    {
        // �J�n���ɉ�]���J�n
        RotateToTarget();
        _scorejoyMove = true;

        // �I�u�W�F�N�g��Rotation���擾
        Vector3 rotation = transform.rotation.eulerAngles;

        // y����Rotation��100�ɐݒ�
        rotation.y = 100f;

        // �V����Rotation��K�p
        transform.rotation = Quaternion.Euler(rotation);

    }
}
