using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkerAntBehaviour : UnitBehaviour
{
    private enum JOB_STATE
    {
        IDLE,
        GETTING_RESOURCE,
        DELIVERING_TO_HIVE,
    };

    JOB_STATE m_JobState;

    [SerializeField]
    GameObject m_ResourceIndicator;
    ResourceBehaviour m_ResourceObject;
    bool m_HasResource = false;

    // Start is called before the first frame update
    void Start()
    {
        m_HasResource = false;
        m_JobState = JOB_STATE.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        m_ResourceIndicator.SetActive(m_HasResource);

        if(m_ResourceObject != null)
        {
            if (m_JobState == JOB_STATE.IDLE)
            {
                SetJobState(JOB_STATE.GETTING_RESOURCE);
            }

            if(m_HasResource)
            {
                SetJobState(JOB_STATE.DELIVERING_TO_HIVE);
            }
        }
        else
        {
            if (m_HasResource)
            {
                SetJobState(JOB_STATE.DELIVERING_TO_HIVE);
            }
            else
            {
                SetJobState(JOB_STATE.IDLE);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
{
        if(collider.gameObject.CompareTag("Building"))
        {
            //If we hit the resource
            ResourceBehaviour resource = collider.gameObject.GetComponent<ResourceBehaviour>();
            if(resource)
            {
                if (m_HasResource == false)
                {
                    resource.DecrementResource(1);
                    m_HasResource = true;
                    SetJobState(JOB_STATE.DELIVERING_TO_HIVE);
                }
            }

            if (m_HiveObject == null)
                return;

            //if we are colliding with the hive.
            if(collider.gameObject.transform.root.gameObject == m_HiveObject.gameObject)
            {
                TEAM_IDENTIFIER hiveTeam = m_HiveObject.GetComponent<TeamDetails>().GetTeam();
                TEAM_IDENTIFIER unitTeam = TeamDetails.GetTeam();

                if (m_HasResource)
                {
                    if (unitTeam == hiveTeam)
                    {
                        m_HiveObject.GetComponent<HiveBehaviour>().AddResourceAmount(1);
                        m_HasResource = false;

                        if (m_ResourceObject != null)
                        {
                            SetJobState(JOB_STATE.GETTING_RESOURCE);
                        }
                        else
                        {
                            SetJobState(JOB_STATE.IDLE);
                        }
                    }
                }
            }
        }
    }

    public override void OnRightClick(GameObject objectHitByRaycast, Vector3 targetPosition, Quaternion rotation)
    {
        ResourceBehaviour resource = objectHitByRaycast.GetComponent<ResourceBehaviour>();
        if (resource)
        {
            if (m_ResourceObject != null)
            {
                m_ResourceObject.RemoveTrackedWorker(this);
                m_ResourceObject = null;
            }

            m_ResourceObject = resource;
            m_ResourceObject.AddTrackedWorker(this);
            SetJobState(JOB_STATE.GETTING_RESOURCE);
        }
        else
        {
            if(m_ResourceObject != null)
            {
                m_ResourceObject.RemoveTrackedWorker(this);
                m_ResourceObject = null;
                SetJobState(JOB_STATE.IDLE);
            }

            Navigation.SetTarget(targetPosition);
        }
    }

    void SetJobState(JOB_STATE newJobState)
    {
        switch (newJobState)
        {
            case JOB_STATE.IDLE:
            {
                if (m_ResourceObject != null)
                {
                    m_ResourceObject.RemoveTrackedWorker(this);
                    m_ResourceObject = null;
                }
                m_HasResource = false;
            }
                break;

            case JOB_STATE.GETTING_RESOURCE:
                Navigation.SetTarget(m_ResourceObject.transform.position);
                break;

            case JOB_STATE.DELIVERING_TO_HIVE:
                Navigation.SetTarget(m_HiveObject.ResourceGatherLocation.position);
                break;

            default:
                break;
        }

        m_JobState = newJobState;
    }
}
