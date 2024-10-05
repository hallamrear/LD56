using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TEAM_IDENTIFIER
{
    UNASSIGNED_TEAM = 0,
    TEAM_A_GREEN = 1,
    TEAM_B_RED = 2,
    TEAM_C_BLUE = 3,
};

public class UnitSettings : MonoBehaviour
{
    public List<Material> m_TeamMaterials;
    TEAM_IDENTIFIER m_TeamID;
    [SerializeField]
    List<GameObject> m_TeamMaterialObjects;

    // Start is called before the first frame update
    void Start()
    {
        m_TeamID = TEAM_IDENTIFIER.UNASSIGNED_TEAM;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeam(TEAM_IDENTIFIER team)
    {
        m_TeamID = team;

        foreach(GameObject obj in m_TeamMaterialObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = m_TeamMaterials[(int)m_TeamID];
        }
    }
}
