using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSwitch : MonoBehaviour {

    public int current;
    public Image image;
    public EquipmentInfo[] items;
    public bool b;

    public void Update()
    {
        if (b)
        {
            Switch(1);
            b = false;
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
}

[System.Serializable]
public class EquipmentInfo
{
    public Sprite sprite;
    public GameObject item;
}
