using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour {

    public List<GameObject> weapons;
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
        weapons[current].SetActive(false);
        current += add;
        if(current >= weapons.Count)
        {
            current = 0;
        }
        else if(current < 0)
        {
            current = weapons.Count - 1;
        }
        weapons[current].SetActive(true);
        weapons[current].GetComponent<Weapon>().active = false;
    }

    public void AddGun(GameObject gun)
    {
        if(availableSlots != 0)
        {
            weapons.Add(gun);
            availableSlots--;
            gun.SetActive(false);
        }
    }
}
