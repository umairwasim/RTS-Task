using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Transform teleporterPointTransform;

    private bool isPlayerEntered = false;

    public bool IsPlayerEntered() => isPlayerEntered = true;

    private void Start()
    {
        TeleportManager.Instance.AddCube(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER))
        {
            //set the bool to true
            isPlayerEntered = true;
            boxCollider.enabled = false;

            //stop the countdown

            //Play particles

            //Play sound

            //Teleport player to the other teleporter's teleporter point
        }
    }
}
