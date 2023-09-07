using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class Unit : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private SpriteRenderer selectionSprite;

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        Init();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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

    public void StartAnimation()
    {
        Debug.Log("agent.velocity.magnitude " + agent.velocity.magnitude);
        if (!agent.isStopped)
            animator.SetBool(IS_WALKING, true);
        else
            animator.SetBool(IS_WALKING, false);
    }
}
