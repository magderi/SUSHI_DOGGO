using UnityEngine;

public class LeftMovement : MonoBehaviour
{
    /// <summary>
    /// タイトルシーンの雲を移動させるスクリプトです
    /// </summary>


    // 以下参照
    public float    speed       = 5f;   // 移動速度
    public float    destroyTime = 10f;  // 破壊までの時間（秒）

    void Update()
    {
        // 左に移動するベクトルを作成
        Vector3 moveDirection = Vector3.left;

        // フレームごとにオブジェクトを移動させる
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // destroyTime秒後に自分を破壊
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
