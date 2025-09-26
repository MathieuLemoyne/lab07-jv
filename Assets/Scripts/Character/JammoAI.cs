using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class JammoAI : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference mousePositionAction;

    [SerializeField] private GameObject destination;
    private NavMeshAgent navMeshAgent;
    // TODO : Compléter cette classe.

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = destination.transform.position;
    }
}