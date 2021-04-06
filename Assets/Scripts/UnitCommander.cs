using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCommander : MonoBehaviour
{
    public GameObject selectionMarkerPrefab;
    public LayerMask layerMask;

    [Header("Components")]
    private UnitSelection unitSelection;
    private Camera cam;

    private void Awake()
    {
        unitSelection = GetComponent<UnitSelection>();
        cam = Camera.main;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && unitSelection.HasUnitsSelected())
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Unit[] selectUnits = unitSelection.GetSelectedUnits();

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                if(hit.collider.CompareTag("Ground"))
                {
                    UnitsMoveToPosition(hit.point, selectUnits);
                    CreateSelectionMarker(hit.point);
                }
            }
        }
    }

    void UnitsMoveToPosition(Vector3 movePosition, Unit[] units)
    {
        Vector3[] destinations = UnitMover.GetUnitGroupDestinations(movePosition, units.Length, 2);

        for(int i = 0; i < units.Length; i++)
        {
            units[i].MoveToPosition(destinations[i]);
        }
    }

    void CreateSelectionMarker(Vector3 position)
    {
        Instantiate(selectionMarkerPrefab, new Vector3(position.x, 0.01f, position.z), Quaternion.identity);
    }
}
