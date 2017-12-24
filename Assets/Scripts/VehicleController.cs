using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class VehicleController : MonoBehaviour {

    NavMeshAgent vehicleAgent;

    // Use this for initialization
    void Start () {
        vehicleAgent = GetComponent<NavMeshAgent>();
    }
	
    public void MoveToPoint(Vector3 targetLocation)
    {
        vehicleAgent.SetDestination(targetLocation);
    }
}
