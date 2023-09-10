using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private List<Teleporter> teleporterList = new();

    //for testing only
    public GameObject player;
    public GameObject teleporterPrefab;

    private bool isEntered = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(InstantiateCube());
    }

    IEnumerator InstantiateCube()
    {
        Instantiate(teleporterPrefab, player.transform.position + new Vector3(5, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Instantiate(teleporterPrefab, player.transform.position + new Vector3(0, 0, 5), Quaternion.identity);
    }

    public void AddCube(Teleporter teleporter)
    {
        teleporterList.Add(teleporter);
        Debug.Log("Teleporter added" + teleporter);
    }

    //private Teleporter OtherTeleporter(Teleporter teleporter)
    //{
    //    return teleporterList.Where(t => t.IsPlayerEntered() == teleporter.IsPlayerEntered());
    //}

    public void Teleport()
    {
        foreach (var teleporter in teleporterList)
        {
            //check if the player entered in the teleporter
            if (teleporter.IsPlayerEntered())
            {
                Transform teleportToTransform = teleporterList[1].teleporterPointTransform;
                player.transform.position = teleportToTransform.position;
                player.GetComponent<Unit>().SetDestinationPoint(teleportToTransform.position);
            }
        }
    }
}
