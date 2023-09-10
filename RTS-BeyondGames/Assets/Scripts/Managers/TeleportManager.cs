using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public static TeleportManager Instance;

    private HashSet<Teleporter> teleporterList = new();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCube(Teleporter teleporter)
    {
        teleporterList.Add(teleporter);
    }

    public void Teleport()
    {
        foreach (var teleporter in teleporterList)
        {
            //check if the player entered in the teleporter
            if(teleporter.IsPlayerEntered())
            {
                
            }
        }
    }    
}
