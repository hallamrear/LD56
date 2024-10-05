using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(UnitNavigation))]
public class UnitBehaviour : MonoBehaviour
{
    public UnitNavigation Navigation;

    private void Awake()
    {
        Navigation = GetComponent<UnitNavigation>();
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
}
