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
        //Sphere���Փ˂����I�u�W�F�N�g��Plane�������ꍇ
        if (collision.gameObject.name == "Sushiinu_salmon")
        {
            // �Փ˂����I�u�W�F�N�g��Rigidbody���擾
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // �Փ˂����I�u�W�F�N�g��Rigidbody���A�^�b�`����Ă��邩�m�F
            if (otherRigidbody != null)
            {
                // Rigidbody��Kinematic�ɐݒ�
                otherRigidbody.isKinematic = true;
            }
            boxCollider.isTrigger = true;
            Debug.Log("CloudHit");
        }
    }

    //OnCollisionExit()
    private void OnCollisionExit(Collision collision)
    {
        //Sphere��Plane���痣�ꂽ�ꍇ
        if (collision.gameObject.name == "Sushiinu_salmon")
        {
            // �Փ˂����I�u�W�F�N�g��Rigidbody���擾
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // �Փ˂����I�u�W�F�N�g��Rigidbody���A�^�b�`����Ă��邩�m�F
            if (otherRigidbody != null)
            {
                // Rigidbody��Kinematic�ɐݒ�
                otherRigidbody.isKinematic = false;
            }

            
            boxCollider.isTrigger = false;
            Debug.Log("CloudExit");
        }
    }
}
