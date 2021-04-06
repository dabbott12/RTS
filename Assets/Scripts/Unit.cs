using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [Header("Components")]
    public GameObject selectionVisual;
    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPosition(Vector3 position)
    {
        navAgent.isStopped = false;
        navAgent.SetDestination(position);
    }

    public void ToggleSelectionVisual(bool toggle)
    {
        selectionVisual.SetActive(toggle);
    }
}
