using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitNavigation : MonoBehaviour
{
    NavMeshAgent m_Agent;
    LayerMask m_GroundMask;

    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_GroundMask = LayerMask.GetMask("Ground");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTarget(Vector3 position)
    {
        m_Agent.SetDestination(position);
    }
}
