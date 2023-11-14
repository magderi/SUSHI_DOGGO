using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPoints; // ����_

    public int resolution = 10; // �Ȑ��̉𑜓x

    // �p�X�̒������v�Z����
    public float GetLength()
    {
        float length = 0f;
        Vector3 previousPoint = GetPointAtTime(0f);

        for (int i = 1; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 currentPoint = GetPointAtTime(t);
            length += Vector3.Distance(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        return length;
    }

    // �Ȑ���̓_���擾����
    public Vector3 GetPointAtTime(float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        // Bezier�Ȑ��̎��Ɋ�Â��ē_���v�Z
        Vector3 point = oneMinusT * oneMinusT * oneMinusT * controlPoints[0].position +
                        3f * oneMinusT * oneMinusT * t * controlPoints[1].position +
                        3f * oneMinusT * t * t * controlPoints[2].position +
                        t * t * t * controlPoints[3].position;

        return point;
    }
}
