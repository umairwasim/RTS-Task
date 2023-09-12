using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(NavMesh))]
public class Unit : MonoBehaviour
{
    [SerializeField] private SpriteRenderer selectionSprite;

    private NavMeshAgent agent;
    private ThirdPersonCharacter character;

    private bool canAnimate = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        Init();
    }

    private void Update()
    {
        if (canAnimate)
            AnimateMovement();
    }

    void Init()
    {
        UnitSelectionManager.Instance.AvailableUnit(this);
        OnDeselected();
    }

    #region OnSelected/Deselected
    public void OnSelected()
    {
        selectionSprite.gameObject.SetActive(true);
        TeleportManager.Instance.SetAgent(agent);
    }

    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
        TeleportManager.Instance.SetAgent(null);
    }
    #endregion

    //Warp agent to the other teleporter's psoition
    public void AgentWarp(Vector3 warpPosition)
    {
        agent.Warp(warpPosition);
    }

    //set the agent's destination position
    public void SetDestinationPoint(Vector3 destination)
    {
        agent.SetDestination(destination);
        canAnimate = true;
    }

    private void AnimateMovement()
    {
        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }
}
