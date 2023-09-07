using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public List<Unit> availableUnits = new();
    public List<Unit> selectedUnits = new();

    private void Awake()
    {
        Instance = this;
    }

    public void SelectUnit(Unit unit)
    {
        if (selectedUnits.Contains(unit))
            return;

        unit.OnSelected();
        selectedUnits.Add(unit);
    }

    public void DeselectUnit(Unit unit)
    {
        unit.OnDeselected();
        selectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        foreach (var unit in selectedUnits)
        {
            unit.OnDeselected();
        }

        selectedUnits.Clear();
    }

    private void Update()
    {
        //Left mouse button clicked
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

        //right mouse button clicked
        if (Input.GetMouseButtonDown(1))
        {
            //set the destination for each selected unit to the mouse world position, om right click
            foreach (var unit in selectedUnits)
            {
                unit.SetDestinationPoint(MouseInput.GetMouseWorldPosition());
            }

        }
    }
}
