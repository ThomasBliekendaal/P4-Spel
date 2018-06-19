using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishAssembly : MonoBehaviour
{
    public Transform hand;
    public GameObject gun;
    private WeaponPartInfo info;
    public GameObject UI;
    public Assembler assembler;
    public GripType element;
    public Text infoText;
    public WeaponSwap swap;
    public GameObject weaponBeforeThing;

    public void Start()
    {
        info = gun.GetComponent<WeaponPartInfo>();
        element = gun.GetComponent<GripType>();
        Time.timeScale = 0;
    }


    public void OnButtonPress()
    {
        GameObject g = Instantiate(weaponBeforeThing, gun.transform.parent.position, gun.transform.rotation, gun.transform.parent);
        element.element = assembler.chosenGrip.GetComponent<GripType>().element;
        element.elementSprite = assembler.chosenGrip.GetComponent<GripType>().elementSprite;
        gun.transform.position = hand.position;
        gun.transform.rotation = hand.rotation;
        gun.transform.localScale = hand.localScale;
        gun.transform.SetParent(hand);
        gun.transform.localScale = new Vector3(1, 1, 1);
        gun.GetComponent<Weapon>().enabled = true;
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
        swap.AddGun(gun);
        swap.Switch(0);
        g.GetComponent<Weapon>().otherPos = gun.GetComponent<Weapon>().otherPos;
        g.GetComponent<Weapon>().ammoInput = gun.GetComponent<Weapon>().ammoInput;
        g.GetComponent<Weapon>().ammoVisual = gun.GetComponent<Weapon>().ammoVisual;
        gun = g;
        assembler.assamblePoint = gun.transform;
        assembler.stock = false;
        assembler.magazine = false;
        assembler.body = false;
        assembler.barrel = false;
        assembler.grip = false;
        assembler.scope = false;
        assembler.chosenBody = null;
        assembler.chosenGrip = null;
        assembler.check = true;
        info = gun.GetComponent<WeaponPartInfo>();
        element = gun.GetComponent<GripType>();
    }

    public void OnHoverEnter()
    {
        infoText.text = "Finish your weapon assembly and start the game.";
    }

    public void OnHoverExit()
    {
        infoText.text = "";
    }
}
