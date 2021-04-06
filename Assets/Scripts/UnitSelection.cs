using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public RectTransform selectionBox;

    public LayerMask unitLayerMask;

    private List<Unit> selectedUnits = new List<Unit>();

    private Vector2 startPosition;

    private Camera cam;
    private Player player;

    private void Awake()
    {
        cam = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ToggleSelectionVisual(false);
            selectedUnits = new List<Unit>();

            TrySelect(Input.mousePosition);

            startPosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }

        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    void TrySelect(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100, unitLayerMask))
        {
            Unit unit = hit.collider.GetComponent<Unit>();

            if(player.IsMyUnit(unit))
            {
                selectedUnits.Add(unit);

                unit.ToggleSelectionVisual(true);
            }
        }
    }

    void ToggleSelectionVisual(bool toggle)
    {
        foreach(Unit unit in selectedUnits)
        {
            unit.ToggleSelectionVisual(toggle);
        }
    }

    void UpdateSelectionBox(Vector2 currentMousePosition)
    {
        if(!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = currentMousePosition.x - startPosition.x;
        float height = currentMousePosition.y - startPosition.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach(Unit unit in player.units)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(unit.transform.position);

            if(screenPosition.x > min.x && screenPosition.x < max.x && screenPosition.y > min.y && screenPosition.y < max.y)
            {
                selectedUnits.Add(unit);
                unit.ToggleSelectionVisual(true);
            }
        }
    }

    public bool HasUnitsSelected()
    {
        return selectedUnits.Count > 0 ? true : false;
    }

    public Unit[] GetSelectedUnits()
    {
        return selectedUnits.ToArray();
    }
}
