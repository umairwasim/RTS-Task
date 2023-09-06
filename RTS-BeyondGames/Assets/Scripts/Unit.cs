using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMesh))]
public class Unit : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer selectionSprite;

    private void Awake()
    {
        Init();
        agent = GetComponent<NavMeshAgent>();
    }

    void Init()
    {
        UnitSelectionManager.Instance.SelectUnit(this);
    }

    public void OnSelected()
    {
        selectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        selectionSprite.gameObject.SetActive(false);
    }
}
