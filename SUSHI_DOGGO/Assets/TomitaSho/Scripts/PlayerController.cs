using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        //Rigidbody‚ðŽæ“¾
        var rb = GetComponent<Rigidbody>();

        //ˆÚ“®‚à‰ñ“]‚à‚µ‚È‚¢‚æ‚¤‚É‚·‚é
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
