using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject objectToGenerate; // 生成するオブジェクト
    public float generationInterval = 3f; // 生成間隔
    public float objectLifetime = 10f; // オブジェクトの寿命

    private float timer = 0f;

    void Update()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 一定時間ごとにオブジェクトを生成
        if (timer >= generationInterval)
        {
            GenerateObject();
            timer = 0f; // タイマーをリセット
        }

        // オブジェクトの寿命が来たら破壊
        if (timer >= objectLifetime)
        {
            DestroyGeneratedObject();
        }
    }

    void GenerateObject()
    {
        // オブジェクトを生成
        GameObject newObject = Instantiate(objectToGenerate, transform.position, Quaternion.identity);

        // 生成されたオブジェクトをこのスクリプトがアタッチされているゲームオブジェクトの子にする（任意）
        newObject.transform.parent = transform;
    }

    void DestroyGeneratedObject()
    {
        // 子オブジェクト（生成されたオブジェクト）を破壊
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // タイマーをリセット
        timer = 0f;
    }
}
