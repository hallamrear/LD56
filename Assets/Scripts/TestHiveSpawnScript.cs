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
                    TrySpawn(TEAM_IDENTIFIER.TEAM_A_GREEN);
                }

                if (Input.GetKeyDown(KeyCode.O))
                {
                    TrySpawn(TEAM_IDENTIFIER.TEAM_B_RED);
                }

                if (Input.GetKeyDown(KeyCode.P))
                {
                    TrySpawn(TEAM_IDENTIFIER.TEAM_C_BLUE);
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
                UnitSettings settings = obj.GetComponent<UnitSettings>();
                if (settings)
                {
                    settings.SetTeam(teamID);
                }

                TestNavController nav = obj.GetComponent<TestNavController>();
                if (nav)
                {
                    nav.SetTarget(BuildingSpawnPoint.position);
                }

                m_TimeSinceSpawn = 0.0f;
            }
        }
    }
}
