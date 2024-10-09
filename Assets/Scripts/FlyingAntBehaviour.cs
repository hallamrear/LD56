using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FlyingAntBehaviour : UnitBehaviour
{
    [SerializeField]
    List<GameObject> m_WingObjects;
    [SerializeField]
    private GameObject m_UnitBody;
    [Range(0.0f, 30.0f)]
    public float FlightHeight;
    private float m_Counter;
    [SerializeField]
    private float m_WingFlapSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Navigation.GetAgent().baseOffset = FlightHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Navigation.GetAgent().baseOffset = FlightHeight;
        ApplyWingRotation();
    }
    
    void ApplyWingRotation()
    {
        m_Counter = m_Counter % 360.0f;
        m_Counter += Time.deltaTime;
        float angle = m_WingFlapSpeed * (Mathf.Sin(m_Counter) * 0.5f);

        foreach (var wings in m_WingObjects)
        {
            wings.transform.RotateAround(wings.transform.position, m_UnitBody.transform.forward, angle);
        }

    }
}
