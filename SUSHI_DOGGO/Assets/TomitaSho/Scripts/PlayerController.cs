using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        //Rigidbody���擾
        var rb = GetComponent<Rigidbody>();

        //�ړ�����]�����Ȃ��悤�ɂ���
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
