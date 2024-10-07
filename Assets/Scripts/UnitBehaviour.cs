using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnitNavigation))]
public class UnitBehaviour : MonoBehaviour
{
    [HideInInspector]
    protected TeamDetails TeamDetails;
    [HideInInspector]
    public UnitNavigation Navigation;

    [HideInInspector]
    protected HiveBehaviour m_HiveObject;

    private void Awake()
    {
        TeamDetails = GetComponent<TeamDetails>();
        Navigation = GetComponent<UnitNavigation>();
        m_HiveObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        UnitSelectionManager.Instance.AllUnits.Add(this);
    }

    private void OnDestroy()
    {
        UnitSelectionManager.Instance.AllUnits.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnRightClick(GameObject objectHitByRaycast, Vector3 targetPosition, Quaternion rotation)
    {
        Navigation.SetTarget(targetPosition);
    }

    public void SetHiveGameObject(HiveBehaviour hive)
    {
        m_HiveObject = hive;
    }
}
