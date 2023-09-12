using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;
    public GameObject teleporterPrefab;

    private NavMeshAgent agent;
    private List<Teleporter> teleportersList = new();

    //for testing only
    //public Unit player;

    public bool CanSpawnTeleporter() => teleportersList.Count <= 2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //just for testing 
        //StartCoroutine(InstantiateCube());
    }

    //IEnumerator InstantiateCube()
    //{
    //    Instantiate(teleporterPrefab, player.transform.position + new Vector3(5, 0, 0), Quaternion.identity);
    //    yield return new WaitForSeconds(3f);
    //    Instantiate(teleporterPrefab, player.transform.position + new Vector3(0, 0, 5), Quaternion.identity);
    //}

    public void AddTeleporter(Teleporter teleporter)
    {
        teleportersList.Add(teleporter);
        Debug.Log("Teleporter added" + teleporter.name);
    }

    //set the selected agent
    public void SetAgent(NavMeshAgent newAgent)
    {
        agent = newAgent;
    }

    //New functions

    public void CreateTeleporter(Vector3 position)
    {
        // Instantiate the teleporter.
        GameObject teleporterGameObject = Instantiate(teleporterPrefab, position, Quaternion.identity);

        if (teleporterGameObject.TryGetComponent(out Teleporter newTeleporter))
        {
            teleportersList.Add(newTeleporter);
        }

        // Check if we have two teleporters, and if so, set their references.
        if (teleportersList.Count == 2)
        {
            SetTeleporterReferences();
        }
    }

    private void SetTeleporterReferences()
    {
        Teleporter teleporter1 = teleportersList[0].GetComponent<Teleporter>();
        Teleporter teleporter2 = teleportersList[1].GetComponent<Teleporter>();

        if (teleporter1 != null && teleporter2 != null)
        {
            teleporter1.SetOtherTeleporter(teleportersList[1]);
            teleporter2.SetOtherTeleporter(teleportersList[0]);
        }
    }

}
