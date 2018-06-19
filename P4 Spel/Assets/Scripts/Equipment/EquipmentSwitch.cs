using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSwitch : MonoBehaviour {

    public Color active;
    public Color notActive;
    public Color nothing;
    public int current;
    public Image image;
    public Text nameInput;
    public Image[] lamps;
    public EquipmentInfo[] items;
    public GameObject weapon;
    public Image fillBar;
    public bool openMenuEquip;

    public void Start()
    {
        Switch(0);
    }

    public void Update()
    {
        if (!openMenuEquip)
        {
            if (Input.GetButtonDown("Switch"))
            {
                Switch(1);
            }
            if (Input.GetButtonDown("Use") && items[current].allow)
            {
                GameObject g = Instantiate(items[current].item, transform.position, transform.rotation);
                g.GetComponent<Rigidbody>().velocity += transform.forward * 8;
                if (g.GetComponent<GunDeploy>())
                {
                    g.GetComponent<GunDeploy>().weapon = weapon;
                    Destroy(g, 30);
                }
                Destroy(g.GetComponent<Rigidbody>(), 2);
                StartCoroutine(TriggerTimer(g.GetComponent<Collider>()));
                StartCoroutine(EquipableCooldown(current));
            }
            if (items[current].currentFill != items[current].cooldown)
            {
                float f = (1 / items[current].cooldown * items[current].currentFill);
                fillBar.fillAmount = f;
            }
        }
    }

    public void Switch(int i)
    {
        current += i;
        if(current >= items.Length)
        {
            current = 0;
        }
        else if(current < 0)
        {
            current = items.Length - 1;
        }
        image.sprite = items[current].sprite;
        nameInput.text = items[current].name;
        float f = (1 / items[current].cooldown * items[current].currentFill);
        fillBar.fillAmount = f;
        for (int i2 = 0; i2 < lamps.Length; i2++)
        {
            if (i2 == current)
            {
                lamps[i2].color = active;
            }
            else if (i2 >= items.Length)
            {
                lamps[i2].color = nothing;
            }
            else
            {
                lamps[i2].color = notActive;
            }
        }
    }

    public IEnumerator TriggerTimer(Collider c)
    {
        yield return new WaitForSeconds(0.1f);
        c.isTrigger = false;
    }
    public IEnumerator EquipableCooldown(int pos)
    {
        items[pos].currentFill = 0;
        StartCoroutine(CooldownVisual(pos));
        items[pos].allow = false;
        yield return new WaitForSeconds(items[pos].cooldown);
        items[pos].allow = true;
    }
    public IEnumerator CooldownVisual(int pos)
    {
        yield return new WaitForSeconds(0.01f);
        items[pos].currentFill += 0.01f;
        if(items[pos].currentFill != items[pos].cooldown)
        {
            StartCoroutine(CooldownVisual(pos));
        }
    }
}

[System.Serializable]
public class EquipmentInfo
{
    public string name;
    public Sprite sprite;
    public GameObject item;
    public float cooldown;
    public bool allow;
    public float currentFill;
}
