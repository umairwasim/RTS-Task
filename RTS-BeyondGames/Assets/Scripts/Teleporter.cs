using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Teleporter : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private BoxCollider boxCollider;
    public Transform teleporterPointTransform;

    public bool isPlayerEntered = false;

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

            //Teleport player to the other teleporter's teleporter point
            TeleportManager.Instance.Teleport();

            //stop the countdown

            //Play particles

            //Play sound

        }
    }
}
