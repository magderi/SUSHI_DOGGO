using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���W������������
        transform.position += new Vector3(0, 0, -5) * Time.deltaTime;
    }
}
