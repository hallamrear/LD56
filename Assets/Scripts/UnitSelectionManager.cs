using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<UnitBehaviour> AllUnits = new List<UnitBehaviour>();
    public List<UnitBehaviour> SelectedUnits = new List<UnitBehaviour>();

    private Vector3 m_BoxDragStart;
    private Vector3 m_BoxDragEnd;
    private bool m_IsDraggingBox;
    RaycastHit[] m_BoxSelectionResults;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("UnitSelection")))
            {
                UnitBehaviour unit = hit.collider.gameObject.GetComponent<UnitBehaviour>();
                if (unit != null)
                {      
                    //If not holding shift, unselect everything.
                    //If we are holding shift, itll add to the selection group.
                    bool shiftHeld = Input.GetButton("AdditionalSelection");
                    AddSingleUnitToSelection(unit, shiftHeld == false);
                }

                m_BoxDragStart = hit.point;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                m_BoxDragStart = hit.point;
                m_BoxDragEnd = hit.point;
                m_IsDraggingBox = true;
            }
        }

        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                m_BoxDragEnd = hit.point;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            m_IsDraggingBox = false;

            Vector3 dragBoxCenter = (m_BoxDragStart + m_BoxDragEnd) / 2.0f;
            Vector3 dragBoxSize = Vector3.zero;
            dragBoxSize.x = Mathf.Abs(m_BoxDragEnd.x - m_BoxDragStart.x);
            dragBoxSize.y = Mathf.Abs(m_BoxDragEnd.y - m_BoxDragStart.y);
            dragBoxSize.z = Mathf.Abs(m_BoxDragEnd.z - m_BoxDragStart.z);

            Collider[] results = Physics.OverlapBox(dragBoxCenter, dragBoxSize / 2.0f, Quaternion.identity, LayerMask.GetMask("UnitSelection"));
            if (results.Length != 0)
            {
                foreach (Collider collider in results)
                {
                    UnitBehaviour unit = collider.gameObject.GetComponent<UnitBehaviour>();
                    if(unit)
                    {
                        AddSingleUnitToSelection(unit, false);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                foreach (UnitBehaviour obj in SelectedUnits)
                {
                    obj.Navigation.SetTarget(hit.point);
                }
            }
        }
    }

    void AddSingleUnitToSelection(UnitBehaviour unit, bool deselectAllBeforeAdding)
    {
        if(deselectAllBeforeAdding)
        {
            DeselectAll();
        }

        SelectedUnits.Add(unit);
    }

    void SelectByBoxDrag()
    {

    }

    public void DeselectAll()
    {
        SelectedUnits.Clear();
    }
    private void OnDrawGizmos()
    {
        foreach(UnitBehaviour obj in SelectedUnits)
        {
            Gizmos.color = UnityEngine.Color.white;
            Gizmos.DrawWireSphere(obj.gameObject.transform.position, 2.5f);
        }

        if (m_IsDraggingBox)
        {
            Vector3 center = (m_BoxDragStart + m_BoxDragEnd) / 2.0f;
            Vector3 size = Vector3.zero;
            size.x = Mathf.Abs(m_BoxDragEnd.x - m_BoxDragStart.x);
            size.y = Mathf.Abs(m_BoxDragEnd.y - m_BoxDragStart.y);
            size.z = Mathf.Abs(m_BoxDragEnd.z - m_BoxDragStart.z);
            Gizmos.DrawCube(center, size);
        }
    }
}
