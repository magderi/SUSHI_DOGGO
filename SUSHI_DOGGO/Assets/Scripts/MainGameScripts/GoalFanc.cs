using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalFanc : MonoBehaviour
{
    [SerializeField]
    private GoalJump _goalJump;

    [SerializeField]
    private SushiJump _shushiJump;

    [SerializeField]
    private StandMoving _standMoving;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalLine"))
        {
            _goalJump.enabled = true;

            _shushiJump.enabled = false;

            _standMoving.enabled = false;
        }
    }
}
