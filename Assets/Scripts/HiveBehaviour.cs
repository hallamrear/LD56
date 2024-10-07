using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TeamDetails))]
public class HiveBehaviour : MonoBehaviour
{
    public TextMeshProUGUI ResourceCounterUIText;

    TeamDetails m_TeamDetails;
    public Transform SpawnLocation;
    public Transform ResourceGatherLocation;
    [SerializeField]
    GameObject m_WorkerPrefab;
    [SerializeField]
    GameObject m_SoldierPrefab;

    int m_ResourceCount;

    private void Awake()
    {
        m_TeamDetails = GetComponent<TeamDetails>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_ResourceCount = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SpawnWorkerAnt();
        }

        if (ResourceCounterUIText)
        {
            ResourceCounterUIText.text = m_ResourceCount.ToString();
        }
    }

    public void SpawnWorkerAnt()
    {
        if (m_WorkerPrefab == null)
            return;

        GameObject worker = Instantiate(m_WorkerPrefab, SpawnLocation.position, Quaternion.identity);
        worker.GetComponent<WorkerAntBehaviour>().SetHiveGameObject(this);
        TeamDetails workerTeamDetails = worker.GetComponent<TeamDetails>();
        workerTeamDetails.SetTeam(m_TeamDetails.GetTeam());
    }


    public void SpawnSoldierAnt()
    {
        if (m_SoldierPrefab == null)
            return;

        GameObject soldier = Instantiate(m_SoldierPrefab, SpawnLocation.position, Quaternion.identity);
        soldier.GetComponent<SoldierAntBehaviour>().SetHiveGameObject(this);
        TeamDetails SoldierTeamDetails = soldier.GetComponent<TeamDetails>();
        SoldierTeamDetails.SetTeam(m_TeamDetails.GetTeam());
    }


    public void AddResourceAmount(int amount)
    {
        m_ResourceCount += amount;
    }

    public void DecreaseResourceAmount(int amount)
    {
        m_ResourceCount -= amount;
    }
}