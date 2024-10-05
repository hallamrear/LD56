using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavController : MonoBehaviour
{
    Camera m_Camera;
    NavMeshAgent m_Agent;

    [SerializeField]
    LayerMask m_GroundMask;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = Camera.main;
        m_Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition); ;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, m_GroundMask))
            {
                m_Agent.SetDestination(hit.point);
            }
        }
    }
}
