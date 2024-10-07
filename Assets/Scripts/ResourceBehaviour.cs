using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResourceBehaviour : MonoBehaviour
{

    [SerializeField]
    ResourceDetailsScriptableObject m_ResourceData;

    bool Exists;
    private string m_Name;
    private string m_Description;
    private bool m_IsInfinite;
    private int m_AmountRemaining;

    List<UnitBehaviour> m_TrackedWorkers;

    private void Awake()
    {
        m_TrackedWorkers = new List<UnitBehaviour>();
    }

    private void OnDestroy()
    {
        m_TrackedWorkers.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_AmountRemaining = m_ResourceData.Amount;
        m_Name = m_ResourceData.Name;
        m_Description = m_ResourceData.Description;
        m_IsInfinite = m_ResourceData.IsInfinite;
        Exists = (m_AmountRemaining > 0) || m_IsInfinite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementResource(int amount)
    {
        if (m_IsInfinite == false)
        {
            m_AmountRemaining -= amount;

            if (m_AmountRemaining <= 0)
            {
                Exists = false;
            }
        }
    }

    public void AddTrackedWorker(UnitBehaviour unit)
    {
        if (m_TrackedWorkers.Contains(unit) == false)
        {
            m_TrackedWorkers.Add(unit);
        }
    }

    public void RemoveTrackedWorker(UnitBehaviour unit)
    {
        if(m_TrackedWorkers.Contains(unit))
        {
            m_TrackedWorkers.Remove(unit);
        }
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            foreach (var ant in m_TrackedWorkers)
            {
                if (ant)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, ant.gameObject.transform.position);
                }
            }
        }
    }
}
