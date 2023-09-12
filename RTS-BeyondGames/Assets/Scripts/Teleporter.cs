using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BoxCollider))]
public class Teleporter : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    public Transform teleporterPointTransform;

    private Teleporter otherTeleporter;

    public bool IsOtherTeleporterExists() => otherTeleporter != null;

    public void SetOtherTeleporter(Teleporter teleporter) => otherTeleporter = teleporter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NavMeshAgent agent))
        {
            //Teleport player to the other teleporter's point
            if (IsOtherTeleporterExists())
            {
                Vector3 otherTeleporterPosition = otherTeleporter.teleporterPointTransform.position;
                agent.Warp(otherTeleporterPosition);

                SoundManager.Instance.PlaySound(SoundManager.Instance.teleporterSound);
                VfxManager.Instance.DisplayVfx(VfxManager.Instance.teleportVfx, otherTeleporterPosition);
            }
        }

        //disable box collider
        boxCollider.enabled = false;

        //Play sound


    }
}
