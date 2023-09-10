using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public List<Unit> availableUnits = new();
    public HashSet<Unit> selectedUnits = new();

    private Unit currentUnit;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsSelectedUnit(Unit unit) => selectedUnits.Contains(unit);

    public Unit CurrentlySelectedUnit => currentUnit;

    #region Select/Deselect Unit
    public void SelectUnit(Unit unit)
    {
        if (selectedUnits.Contains(unit))
            return;

        unit.OnSelected();

        currentUnit = unit;
        selectedUnits.Add(unit);
    }

    public void DeselectUnit(Unit unit)
    {
        unit.OnDeselected();
        currentUnit = null;
        selectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        foreach (var unit in selectedUnits)
        {
            unit.OnDeselected();
        }
        currentUnit = null;
        selectedUnits.Clear();
    }
    #endregion

    private void Update()
    {
        //Left mouse button clicked
        UnitSelect();

        //right mouse button clicked
        UnitMove();
    }

    private void UnitMove()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //set the destination for each selected unit to the mouse world position, om right click
            foreach (var unit in selectedUnits)
            {
                unit.SetDestinationPoint(MouseInput.GetMouseWorldPosition());
                //unit.StartAnimation();
            }

        }
    }

    private void UnitSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //deselect all and clear the selected list
            DeselectAll();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                if (raycastHit.transform.TryGetComponent(out Unit unit))
                {
                    //select the unit on left click
                    SelectUnit(unit);
                }
            }
        }
    }

}
