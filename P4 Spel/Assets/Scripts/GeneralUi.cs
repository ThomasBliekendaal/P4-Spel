using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUi : MonoBehaviour {
    public GameObject escapeMenu;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Escape"))
        {
            escapeMenu.SetActive(!escapeMenu.activeInHierarchy);
        }
	}
}
