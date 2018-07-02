using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWeaponCreation : MonoBehaviour {
    public GameObject weaponUI;
    public GameObject normalUI;
    public Transform hand;
    public AudioSource intro;

	public void OnButtonPress()
    {
        weaponUI.SetActive(false);
        normalUI.SetActive(true);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hand.transform.parent.parent.GetComponent<PlayerScript>().enabled = true;
        hand.parent.gameObject.SetActive(true);
        intro.Play();
    }
}
