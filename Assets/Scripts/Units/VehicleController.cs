using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class VehicleController : MonoBehaviour {

    NavMeshAgent vehicleAgent;
	public float relaxDistance = 1;
	private Vector3 target;
    // Use this for initialization
    void Start () {
        vehicleAgent = GetComponent<NavMeshAgent>();
		target = Vector3.zero;
    }
	private void Update()
	{
		// Move to a poin within relaxDistance
		if (GetComponent<Vehicle>().isMoving && Vector3.Distance(transform.position, target) < relaxDistance)
		{
			vehicleAgent.Stop();
			GetComponent<Vehicle>().isMoving = false;
		}

		// Remove selection marker when not moving
		if(!GetComponent<Vehicle>().isSelected && !GetComponent<Vehicle>().isMoving)
		{
			GetComponent<Transform>().Find("SelectionCirclePrefab").gameObject.SetActive(false);
		}
	}
	public void MoveToPoint(Vector3 targetLocation)
    {
		//relaxDistance = Random.Range(0, 2);
		target = targetLocation;
        vehicleAgent.SetDestination(targetLocation);
		vehicleAgent.Resume();
		GetComponent<Vehicle>().isMoving = true;
    }
}
