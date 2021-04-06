using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    public static Vector3[] GetUnitGroupDestinations(Vector3 moveToPosition, int numUnits, float unitGap)
    {
        Vector3[] destinations = new Vector3[numUnits];

        int rows = Mathf.RoundToInt(Mathf.Sqrt(numUnits));
        int columns = Mathf.CeilToInt((float)numUnits / (float)rows);

        int currentRow = 0;
        int currentColumn = 0;

        float width = (float)(rows - 1 * unitGap);
        float length = (float)(columns - 1 * unitGap);

        for(int i = 0; i < numUnits; i++)
        {
            destinations[i] = moveToPosition + (new Vector3(currentRow, 0, currentColumn) * unitGap) - new Vector3(length / 2, 0, width /2);
            currentColumn++;

            if(currentColumn == rows)
            {
                currentColumn = 0;
                currentRow++;
            }
        }

        return destinations;
    }
}
