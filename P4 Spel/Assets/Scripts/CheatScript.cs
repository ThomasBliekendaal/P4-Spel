using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatScript : MonoBehaviour {

    public GameObject menu;
    public bool cheatsEnabled;
    private GameObject player;
    public GameObject wavemanager;
    private GameObject weapon;
    private bool unlimitedAmmo;
    private GameObject equipmentSwitch;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        weapon = GameObject.Find("Weapon");
        Weapon wP = weapon.GetComponent<Weapon>();
        if (unlimitedAmmo)
        {
            wP.currentAmmo = 30;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ToggleMenu();
        }
        if (cheatsEnabled)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
        if (cheatsEnabled)
        {
            WaveManager wm = wavemanager.GetComponent<WaveManager>();
            if (Input.GetKeyDown(KeyCode.K))
            {
                wm.KillAll();
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                player.GetComponent<PlayerScript>().ToggleGod();
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                //this should remove all cooldowns
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                unlimitedAmmo = !unlimitedAmmo;
            }
            if (Input.GetKeyDown(KeyCode.Minus))
            {
                print("Doesn't Work yet");
                //SceneManager.LoadScene()
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                wm.NextWave();
            }
        }
	}

    public void ToggleMenu()
    {
        cheatsEnabled = !cheatsEnabled;
    }
}
