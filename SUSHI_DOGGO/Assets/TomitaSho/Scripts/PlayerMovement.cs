using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public BezierCurve bezierCurve; // BezierCurveを参照する変数

    public float speed = 5f; // プレイヤーの移動速度

    private float t = 0f; // 曲線上の位置を示すパラメータ

    void Update()
    {
        // パラメータtを更新して曲線上を進む
        t += Time.deltaTime * speed / bezierCurve.GetLength();

        // 曲線上の位置を取得
        Vector3 position = bezierCurve.GetPointAtTime(t);

        // プレイヤーを移動させる
        transform.position = position;

        // カメラを曲線に追従させる
        UpdateCameraPosition();
    }

    void UpdateCameraPosition()
    {
        // カメラの位置をプレイヤーの少し前方に設定
        Vector3 cameraPosition = bezierCurve.GetPointAtTime(t + 0.1f);

        // カメラの方向をプレイヤーの進行方向に向ける
        transform.LookAt(cameraPosition);
    }
}
