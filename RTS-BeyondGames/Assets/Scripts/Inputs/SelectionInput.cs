using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionInput : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private RectTransform SelectionBox;
    [SerializeField]
    private LayerMask UnitLayers;
    [SerializeField]
    private LayerMask FloorLayers;
    [SerializeField]
    private float DragDelay = 0.1f;

    private float MouseDownTime;
    private Vector2 StartMousePosition;

    private HashSet<Unit> newlySelectedUnits = new HashSet<Unit>();
    private HashSet<Unit> deselectedUnits = new HashSet<Unit>();

    private void Update()
    {
        HandleSelectionInputs();
        HandleMovementInputs();
    }

    private void HandleMovementInputs()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1) && UnitSelectionManager.Instance.selectedUnits.Count > 0)
        {
            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, FloorLayers))
            {
                foreach (Unit unit in UnitSelectionManager.Instance.selectedUnits)
                {
                    unit.SetDestinationPoint(Hit.point);
                }
            }
        }
    }

    private void HandleSelectionInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(true);
            StartMousePosition = Input.mousePosition;
            MouseDownTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.Mouse0) && MouseDownTime + DragDelay < Time.time)
        {
            ResizeSelectionBox();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            SelectionBox.sizeDelta = Vector2.zero;
            SelectionBox.gameObject.SetActive(false);

            foreach (Unit newUnit in newlySelectedUnits)
            {
                UnitSelectionManager.Instance.SelectUnit(newUnit);
            }
            foreach (Unit deselectedUnit in deselectedUnits)
            {
                UnitSelectionManager.Instance.DeselectUnit(deselectedUnit);
            }

            newlySelectedUnits.Clear();
            deselectedUnits.Clear();

            if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, UnitLayers)
                && hit.collider.TryGetComponent(out Unit unit))
            {
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    if (UnitSelectionManager.Instance.IsSelectedUnit(unit))
                    {
                        UnitSelectionManager.Instance.DeselectUnit(unit);
                    }
                    else
                    {
                        UnitSelectionManager.Instance.SelectUnit(unit);
                    }
                }
                else
                {
                    UnitSelectionManager.Instance.DeselectAll();
                    UnitSelectionManager.Instance.SelectUnit(unit);
                }
            }
            else if (MouseDownTime + DragDelay > Time.time)
            {
                UnitSelectionManager.Instance.DeselectAll();
            }

            MouseDownTime = 0;
        }
    }

    private void ResizeSelectionBox()
    {
        float width = Input.mousePosition.x - StartMousePosition.x;
        float height = Input.mousePosition.y - StartMousePosition.y;

        SelectionBox.anchoredPosition = StartMousePosition + new Vector2(width / 2, height / 2);
        SelectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));

        Bounds bounds = new Bounds(SelectionBox.anchoredPosition, SelectionBox.sizeDelta);

        for (int i = 0; i < UnitSelectionManager.Instance.availableUnits.Count; i++)
        {
            if (UnitIsInSelectionBox(Camera.WorldToScreenPoint(UnitSelectionManager.Instance.availableUnits[i].transform.position), bounds))
            {
                if (!UnitSelectionManager.Instance.IsSelectedUnit(UnitSelectionManager.Instance.availableUnits[i]))
                {
                    newlySelectedUnits.Add(UnitSelectionManager.Instance.availableUnits[i]);
                }
                deselectedUnits.Remove(UnitSelectionManager.Instance.availableUnits[i]);
            }
            else
            {
                deselectedUnits.Add(UnitSelectionManager.Instance.availableUnits[i]);
                newlySelectedUnits.Remove(UnitSelectionManager.Instance.availableUnits[i]);
            }
        }
    }

    private bool UnitIsInSelectionBox(Vector2 Position, Bounds Bounds)
    {
        return Position.x > Bounds.min.x && Position.x < Bounds.max.x
            && Position.y > Bounds.min.y && Position.y < Bounds.max.y;
    }
}

