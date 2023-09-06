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

        selectedUnits.Add(unit);
    }

    public void DeselectUnit(Unit unit)
    {
        selectedUnits.Remove(unit);
    }

    public void DeselectAll()
    {
        selectedUnits.Clear();
    }
}
