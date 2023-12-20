using UnityEngine;
using System.Collections;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class DogController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;

    private NavMeshAgent m_Agent;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        m_Agent.SetDestination(m_Target.position);
    }
}