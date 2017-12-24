using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public bool IsSelected = false;

    public Outline OutlineEffect;

	// Use this for initialization
	void Start () {
        OutlineEffect = GetComponent<Outline>();
	}
	
	// Update is called once per frame
	void Update () {
        DrawSelection();
	}

    public void DrawSelection()
    {
        if (IsSelected)
        {
            OutlineEffect.eraseRenderer = false;
        }
        else
        {
            OutlineEffect.eraseRenderer = true;
        }
    }
}
