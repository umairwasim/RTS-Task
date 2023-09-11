using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMesh))]
public class Unit : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private SpriteRenderer selectionSprite;

    private NavMeshAgent agent;
    private ThirdPersonCharacter character;

    private void Awake()
    {
        Init();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
    }

    void Init()
    {
        //UnitSelectionManager.Instance.SelectUnit(this);
        OnDeselected();
    }

    public void OnSelected()
    {
        selectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
    }

    public void SetDestinationPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void AnimateMovement()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }
}
