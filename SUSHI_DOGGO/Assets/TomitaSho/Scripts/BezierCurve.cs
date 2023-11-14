using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPoints; // 制御点

    public int resolution = 10; // 曲線の解像度

    // パスの長さを計算する
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

    // 曲線上の点を取得する
    public Vector3 GetPointAtTime(float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        // Bezier曲線の式に基づいて点を計算
        Vector3 point = oneMinusT * oneMinusT * oneMinusT * controlPoints[0].position +
                        3f * oneMinusT * oneMinusT * t * controlPoints[1].position +
                        3f * oneMinusT * t * t * controlPoints[2].position +
                        t * t * t * controlPoints[3].position;

        return point;
    }
}
