using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HiveSpawnScript : MonoBehaviour
{
    public Transform BuildingSpawnPoint;
    public float TimeBetweenSpawns;
    private float m_TimeSinceSpawn;
    [SerializeField]
    GameObject m_TestObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(TimeBetweenSpawns > 0.0f)
        {
            m_TimeSinceSpawn += Time.deltaTime;

            if (m_TimeSinceSpawn > TimeBetweenSpawns)
            {
                if(Input.GetKeyDown(KeyCode.U))
                {
                    TrySpawn(TEAM_IDENTIFIER.UNASSIGNED_TEAM);
                }

                if (Input.GetKeyDown(KeyCode.I))
                {
                    TrySpawn(TEAM_IDENTIFIER.TEAM_A);
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    TrySpawn(TEAM_IDENTIFIER.TEAM_B);
                }

                if (Input.GetKeyDown(KeyCode.P))
                {
                    TrySpawn(TEAM_IDENTIFIER.TEAM_C);
                }
            }
        }
    }

    private void TrySpawn(TEAM_IDENTIFIER teamID)
    {
        if (m_TestObject != null && BuildingSpawnPoint != null)
        {
            GameObject obj = Instantiate(m_TestObject, BuildingSpawnPoint.position, Quaternion.identity);

            if (obj)
            {
                TeamDetails settings = obj.GetComponent<TeamDetails>();
                if (settings)
                {
                    settings.SetTeam(teamID);
                }

                UnitBehaviour behaviour = obj.GetComponent<UnitBehaviour>();
                if (behaviour)
                {
                    behaviour.Navigation.SetTarget(BuildingSpawnPoint.position + (Vector3.forward * 0.05f));
                }

                m_TimeSinceSpawn = 0.0f;
            }
        }
    }
}
