using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{

    [SerializeField]
    private BoxCollider boxCollider;


    //OnCollisionEnter()
    private void OnCollisionEnter(Collision collision)
    {
        //Sphereが衝突したオブジェクトがPlaneだった場合
        if (collision.gameObject.name == "Sushiinu_salmon")
        {
            // 衝突したオブジェクトのRigidbodyを取得
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // 衝突したオブジェクトにRigidbodyがアタッチされているか確認
            if (otherRigidbody != null)
            {
                // RigidbodyをKinematicに設定
                otherRigidbody.isKinematic = true;
            }
            boxCollider.isTrigger = true;
            Debug.Log("CloudHit");
        }
    }

    //OnCollisionExit()
    private void OnCollisionExit(Collision collision)
    {
        //SphereがPlaneから離れた場合
        if (collision.gameObject.name == "Sushiinu_salmon")
        {
            // 衝突したオブジェクトのRigidbodyを取得
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // 衝突したオブジェクトにRigidbodyがアタッチされているか確認
            if (otherRigidbody != null)
            {
                // RigidbodyをKinematicに設定
                otherRigidbody.isKinematic = false;
            }

            
            boxCollider.isTrigger = false;
            Debug.Log("CloudExit");
        }
    }
}
