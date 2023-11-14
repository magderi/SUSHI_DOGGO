using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public BezierCurve bezierCurve; // BezierCurve���Q�Ƃ���ϐ�

    public float speed = 5f; // �v���C���[�̈ړ����x

    private float t = 0f; // �Ȑ���̈ʒu�������p�����[�^

    void Update()
    {
        // �p�����[�^t���X�V���ċȐ����i��
        t += Time.deltaTime * speed / bezierCurve.GetLength();

        // �Ȑ���̈ʒu���擾
        Vector3 position = bezierCurve.GetPointAtTime(t);

        // �v���C���[���ړ�������
        transform.position = position;

        // �J�������Ȑ��ɒǏ]������
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // �J�����̈ʒu���v���C���[�̏����O���ɐݒ�
        Vector3 cameraPosition = bezierCurve.GetPointAtTime(t + 0.1f);

        // �J�����̕������v���C���[�̐i�s�����Ɍ�����
        transform.LookAt(cameraPosition);
    }
}
