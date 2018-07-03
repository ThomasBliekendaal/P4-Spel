using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUI : MonoBehaviour {

    public GameObject weaponUI;

    public void Start()
    {
        Time.timeScale = 0;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.SetActive(false);
            weaponUI.SetActive(true);
        }
    }
}
