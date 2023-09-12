using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Teleporter : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private BoxCollider boxCollider;
    public Transform teleporterPointTransform;

    private Teleporter otherTeleporter;

    public bool IsOtherTeleporterExists() => otherTeleporter != null;

    private void Start()
    {
        //for testing only 
        //TeleportManager.Instance.AddTeleporter(this);
    }

    public void SetOtherTeleporter(Teleporter teleporter)
    {
        otherTeleporter = teleporter;
        Debug.Log("Other Teleporter Name " + otherTeleporter.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag(PLAYER))
        {
            if (other.TryGetComponent(out NavMeshAgent agent))
            {
                //Teleport player to the other teleporter's point
                if (IsOtherTeleporterExists())
                {
                    agent.Warp(otherTeleporter.teleporterPointTransform.position);
                }
            }

            //disable box collider
            boxCollider.enabled = false;

            //TeleportManager.Instance.Teleport();

            //stop the countdown

            //Play particles

            //Play sound

        }
    }
}
