using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {
    
    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit) && hit.transform.tag != "Vehicle")
            {
                foreach (var vehicleUnit in GameObject.FindGameObjectsWithTag("Vehicle"))
                {
                    if (vehicleUnit.GetComponent<Vehicle>().isSelected)
                    {
						//Move to target location
						vehicleUnit.GetComponent<VehicleController>().MoveToPoint(hit.point);
                        //Deselect
                        vehicleUnit.GetComponent<Vehicle>().isSelected = false;
                    }
                }
            }
        }
	}
}
