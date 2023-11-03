using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    private float speed = 10.0f;
    private int count = 1;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] Transform pointC;
    [SerializeField] Transform pointD;
    [SerializeField] Transform pointE;
    [SerializeField] Transform pointF;

    void Update()
    {
        if (count == 0)
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
        else if (count == 1)
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
        else if (count == 2)
            transform.position = Vector3.MoveTowards(transform.position, pointC.position, speed * Time.deltaTime);
        else if (count == 3)
            transform.position = Vector3.MoveTowards(transform.position, pointD.position, speed * Time.deltaTime);
        else if (count == 4)
            transform.position = Vector3.MoveTowards(transform.position, pointE.position, speed * Time.deltaTime);
        else if (count == 5)
            transform.position = Vector3.MoveTowards(transform.position, pointF.position, speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PointA")
            count = 1;
        else if (other.gameObject.name == "PointB")
            count = 2;
        else if (other.gameObject.name == "PointC")
            count = 3;
        else if (other.gameObject.name == "PointD")
            count = 4;
        else if (other.gameObject.name == "PointE")
            count = 5;
        else if (other.gameObject.name == "PointF")
            count = 6;
    }

 
}

