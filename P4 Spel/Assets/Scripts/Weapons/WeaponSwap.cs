using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwap : MonoBehaviour {

    public List<GameObject> weapons;
    public List<WeaponSlotImages> weaponInfoSlot;
    public int current;
    public Transform hand;
    public int availableSlots;


    public void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if(Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                Switch(-1);
            }
            else
            {
                Switch(1);
            }
        }
    }

    public void Switch(int add)
    {
        if(weapons.Count > 1)
        {
            weapons[current].SetActive(false);
            weaponInfoSlot[current].highlight.SetActive(false);
            if (weapons[current].GetComponent<Weapon>().activeReload)
            {
                weapons[current].GetComponent<Weapon>().activeReload = false;
                weapons[current].GetComponent<Weapon>().ammoVisual.fillAmount = 0;
            }
            current += add;
            if (current >= weapons.Count)
            {
                current = 0;
            }
            else if (current < 0)
            {
                current = weapons.Count - 1;
            }
            weapons[current].SetActive(true);
            weaponInfoSlot[current].highlight.SetActive(true);
            weapons[current].GetComponent<Weapon>().active = false;
        }
    }

    public void AddGun(GameObject gun)
    {
        if(availableSlots != 0)
        {
            weaponInfoSlot[weapons.Count].iconInputElement.sprite = gun.GetComponent<GripType>().elementSprite;
            weaponInfoSlot[weapons.Count].iconInputElement.gameObject.SetActive(true);
            foreach (Transform child in gun.transform)
            {
                if (child.GetComponent<BodyAttachment>())
                {
                    weaponInfoSlot[weapons.Count].iconInputWeapon.sprite = child.GetComponent<BodyAttachment>().sprite;
                    weaponInfoSlot[weapons.Count].iconInputWeapon.gameObject.SetActive(true);
                }
            }
            weapons.Add(gun);
            availableSlots--;
            if (weapons.Count != 1)
            {
                gun.SetActive(false);
            }
            else
            {
                weapons[current].SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class WeaponSlotImages
{
    public GameObject highlight;
    public Image iconInputWeapon;
    public Image iconInputElement;
}