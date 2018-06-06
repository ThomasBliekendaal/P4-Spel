using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSwitch : MonoBehaviour {

    public int current;
    public Image image;
    public EquipmentInfo[] items;
    public GameObject weapon;

    public void Update()
    {
        if (Input.GetButtonDown("Switch"))
        {
            Switch(1);
        }
        if (Input.GetButtonDown("Use"))
        {
            GameObject g = Instantiate(items[current].item, transform.position, transform.rotation);
            g.GetComponent<Rigidbody>().velocity += transform.forward * 8;
            if (g.GetComponent<GunDeploy>())
            {
                g.GetComponent<GunDeploy>().weapon = weapon;
            }
            Destroy(g.GetComponent<Rigidbody>(), 2);
        }
    }

    public void Switch(int i)
    {
        current++;
        if(current >= items.Length)
        {
            current = 0;
        }
        else if(current < 0)
        {
            current = items.Length - 1;
        }
        image.sprite = items[current].sprite;
    }

    public IEnumerator TriggerTimer;
}

[System.Serializable]
public class EquipmentInfo
{
    public Sprite sprite;
    public GameObject item;
}
