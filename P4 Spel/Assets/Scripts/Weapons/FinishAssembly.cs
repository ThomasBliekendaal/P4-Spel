using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAssembly : MonoBehaviour
{
    public Transform hand;
    public GameObject gun;
    private WeaponPartInfo info;
    public GameObject UI;

    public void Start()
    {
        info = gun.GetComponent<WeaponPartInfo>();
        Time.timeScale = 0;
    }


    public void OnButtonPress()
    {
        UI.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hand.transform.parent.parent.GetComponent<PlayerScript>().enabled = true;
        Time.timeScale = 1;
        hand.parent.gameObject.SetActive(true);
        gun.transform.position = hand.position;
        gun.transform.rotation = hand.rotation;
        gun.transform.localScale = hand.localScale;
        gun.transform.SetParent(hand);
        gun.GetComponent<Weapon>().enabled = true;
        Destroy(transform.parent.gameObject);
        foreach (Transform child in gun.transform)
        {
            if (child.GetComponent<WeaponPartInfo>())
            {
                info.damage += child.GetComponent<WeaponPartInfo>().damage;
                info.fireRate += child.GetComponent<WeaponPartInfo>().fireRate;
                info.reloadSpeed += child.GetComponent<WeaponPartInfo>().reloadSpeed;
                info.accuracy += child.GetComponent<WeaponPartInfo>().accuracy;
                info.stability += child.GetComponent<WeaponPartInfo>().stability;
                info.weight += child.GetComponent<WeaponPartInfo>().weight;
                info.swapSpeed += child.GetComponent<WeaponPartInfo>().swapSpeed;
                info.ammo += child.GetComponent<WeaponPartInfo>().ammo;
                info.ammoType = child.GetComponent<WeaponPartInfo>().ammoType;
            }
            foreach (Transform otherChild in child)
            {
                foreach (Transform anotherChild in otherChild)
                {
                    if (anotherChild.GetComponent<WeaponPartInfo>())
                    {
                        info.damage += anotherChild.GetComponent<WeaponPartInfo>().damage;
                        info.fireRate += anotherChild.GetComponent<WeaponPartInfo>().fireRate;
                        info.reloadSpeed += anotherChild.GetComponent<WeaponPartInfo>().reloadSpeed;
                        info.accuracy += anotherChild.GetComponent<WeaponPartInfo>().accuracy;
                        info.stability += anotherChild.GetComponent<WeaponPartInfo>().stability;
                        info.weight += anotherChild.GetComponent<WeaponPartInfo>().weight;
                        info.swapSpeed += anotherChild.GetComponent<WeaponPartInfo>().swapSpeed;
                        info.ammo += anotherChild.GetComponent<WeaponPartInfo>().ammo;
                    }
                    foreach (Transform no in anotherChild)
                    {
                        foreach (Transform no2 in no)
                        {
                            if (no2.GetComponent<WeaponPartInfo>())
                            {
                                info.damage += no2.GetComponent<WeaponPartInfo>().damage;
                                info.fireRate += no2.GetComponent<WeaponPartInfo>().fireRate;
                                info.reloadSpeed += no2.GetComponent<WeaponPartInfo>().reloadSpeed;
                                info.accuracy += no2.GetComponent<WeaponPartInfo>().accuracy;
                                info.stability += no2.GetComponent<WeaponPartInfo>().stability;
                                info.weight += no2.GetComponent<WeaponPartInfo>().weight;
                                info.swapSpeed += no2.GetComponent<WeaponPartInfo>().swapSpeed;
                                info.ammo += no2.GetComponent<WeaponPartInfo>().ammo;
                            }
                        }
                    }
                }
            }
        }
    }
}
