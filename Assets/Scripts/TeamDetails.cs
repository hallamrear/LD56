using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TEAM_IDENTIFIER
{
    UNASSIGNED_TEAM = 0,
    TEAM_A = 1,
    TEAM_B = 2,
    TEAM_C = 3,
};
public class TeamDetails : MonoBehaviour
{
    [SerializeField]
    TeamMaterialsScriptableObject m_TeamMaterials;
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

        Material matToApply = null;
        
            switch (m_TeamID)
        {
            case TEAM_IDENTIFIER.UNASSIGNED_TEAM:
                matToApply = m_TeamMaterials.Unassigned;
                break;
            case TEAM_IDENTIFIER.TEAM_A:
                matToApply = m_TeamMaterials.TeamA;
                break;
            case TEAM_IDENTIFIER.TEAM_B:
                matToApply = m_TeamMaterials.TeamB;
                break;
            case TEAM_IDENTIFIER.TEAM_C:
                matToApply = m_TeamMaterials.TeamC;
                break;
            default:
                break;
        }

        foreach (GameObject obj in m_TeamMaterialObjects)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            renderer.material = matToApply;            
        }
    }
}
