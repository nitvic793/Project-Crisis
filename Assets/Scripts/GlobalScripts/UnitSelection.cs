using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UnitSelection : MonoBehaviour {
    bool isSelecting = false;
    Vector3 mousePositionBegin;

    public GameObject selectionCirclePrefab;

    private List<GameObject> selectedObjects = new List<GameObject>();

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            selectedObjects.Clear();
            RaycastHit hit = new RaycastHit();
            isSelecting = true;
            mousePositionBegin = Input.mousePosition;
            
            foreach (var selectableObject in GameObject.FindGameObjectsWithTag("Vehicle"))
            {
                if (selectableObject.GetComponent<Vehicle>().selectionCircle != null)
                {
                    Destroy(selectableObject.GetComponent<Vehicle>().selectionCircle.gameObject);
                    selectableObject.GetComponent<Vehicle>().selectionCircle = null;
                }
            }

            foreach (var selectableObject in GameObject.FindGameObjectsWithTag("Building"))
            {
                selectableObject.GetComponent<Building>().IsSelected = false;
            }

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.tag == "Vehicle")
                {
                    GameObject selectedVehicle = hit.transform.gameObject;
                    selectedObjects.Add(hit.transform.gameObject);
                    if (selectedVehicle.GetComponent<Vehicle>().selectionCircle == null)
                    {
                        Debug.Log("Test");
                        selectedVehicle.GetComponent<Vehicle>().selectionCircle = Instantiate(selectionCirclePrefab);
                        selectedVehicle.GetComponent<Vehicle>().selectionCircle.transform.SetParent(selectedVehicle.transform, false);
                        selectedVehicle.GetComponent<Vehicle>().selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
                else if(hit.transform.tag == "Building")
                {
                    hit.transform.GetComponent<Building>().IsSelected = true;
                }
            }
        }
        
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            
            foreach (var selectableObject in GameObject.FindGameObjectsWithTag("Vehicle"))
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject);
                }
            }

#region Debug
            /*
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
            foreach (var selectedObject in selectedObjects)
                sb.AppendLine("-> " + selectedObject.gameObject.name);
            Debug.Log(sb.ToString());
            */
#endregion
            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in GameObject.FindGameObjectsWithTag("Vehicle"))
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    if (selectableObject.GetComponent<Vehicle>().selectionCircle == null)
                    {
                        selectableObject.GetComponent<Vehicle>().selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.GetComponent<Vehicle>().selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.GetComponent<Vehicle>().selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds = SelectionRectangle.GetViewportBounds(camera, mousePositionBegin, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = SelectionRectangle.GetScreenRectangle(mousePositionBegin, Input.mousePosition);
            SelectionRectangle.DrawScreenRectangle(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            SelectionRectangle.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
