using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DisableSpawnButton();
	}
	
	// Update is called once per frame
	void Update () {
        DisableSpawnButton();
        var buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (var building in buildings)
        {
            if (building.GetComponent<Building>().IsSelected)
            {
                EnableSpawnButton();
            }
        }
    }

    void EnableSpawnButton()
    {
        GameObject.Find("SpawnButton").GetComponent<Button>().interactable = true;
    }

    void DisableSpawnButton()
    {
        GameObject.Find("SpawnButton").GetComponent<Button>().interactable = false;
    }
}
