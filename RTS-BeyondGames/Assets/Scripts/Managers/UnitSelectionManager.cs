using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; private set; }

    public List<Unit> availableUnits = new();
    public List<Unit> selectedUnits = new();

    private Unit currentUnit;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsSelectedUnit(Unit unit) => selectedUnits.Contains(unit);

    public Unit CurrentlySelectedUnit => currentUnit;

    #region Select/Deselect/Available Unit
    public void AvailableUnit(Unit unit)
    {
        if (availableUnits.Contains(unit))
            return;

        availableUnits.Add(unit);
    }

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
        Select();
        //right mouse button clicked
        Move();
    }

    private void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //deselect all and clear the selected list
            DeselectAll();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit))
            {
                //check if it can spawn Teleporter, then spawn it, twice
                if (TeleportManager.Instance.GetCanSpawnTeleporter())
                {
                    //Prevemt the case when clicking on Unit r teleporter to spawn Teleport object
                    if (raycastHit.transform.TryGetComponent(out Teleporter teleporter) ||
                        raycastHit.transform.TryGetComponent(out Unit uniy))
                        return;

                    TeleportManager.Instance.GenerateTeleporter(raycastHit.point);

                    SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClickSound);

                }
                else // otherwise select the Unit for navigation 
                {
                    if (raycastHit.transform.TryGetComponent(out Unit unit))
                    {
                        //select the unit on left click
                        SelectUnit(unit);

                        SoundManager.Instance.PlaySound(SoundManager.Instance.buttonClickSound);

                    }
                }
            }
        }
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //set the destination for each selected unit to the mouse world position, om right click
            foreach (var unit in selectedUnits)
            {
                unit.SetDestinationPoint(MouseInput.GetMouseWorldPosition());
                // unit.AnimateMovement();
            }

        }
    }

}
