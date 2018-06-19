using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUi : MonoBehaviour {
    public GameObject escapeMenu;
    public GameObject playerUi;
    public GameObject playerWep;
    public GameObject optionsMenu;
    public Slider sensitivitySlider;
    

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Escape") && !optionsMenu.activeInHierarchy)
        {
            ResumeGame();
        }
        if (Input.GetButtonDown("Escape") && optionsMenu.activeInHierarchy)
        {
            ResumeWithOptions();
        }
        if (optionsMenu.activeInHierarchy)
        {
            playerUi.GetComponent<PlayerScript>().sensitivity = sensitivitySlider.value;
        }
	}
    public void ResumeGame()
    {
        escapeMenu.SetActive(!escapeMenu.activeInHierarchy);
        if (escapeMenu.activeInHierarchy)
        {
            Time.timeScale = 0;
            playerUi.GetComponent<PlayerScript>().openMenu = true;
            playerUi.GetComponent<EquipmentSwitch>().openMenuEquip = true;
            playerWep.GetComponent<Weapon>().openMenuWeap = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            playerUi.GetComponent<PlayerScript>().openMenu = false;
            playerUi.GetComponent<EquipmentSwitch>().openMenuEquip = false;
            playerWep.GetComponent<Weapon>().openMenuWeap = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void ResumeWithOptions()
    {
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
        Time.timeScale = 1;
        playerUi.GetComponent<PlayerScript>().openMenu = false;
        playerUi.GetComponent<EquipmentSwitch>().openMenuEquip = false;
        playerWep.GetComponent<Weapon>().openMenuWeap = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
