using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assembler : MonoBehaviour
{

    public GameObject[] bodys;
    public GameObject chosenBody;
    public GameObject chosenGrip;
    public WeaponSwap swap;
    public GameObject startGame;
    public GameObject weapon;

    public GameObject panel;

    public Transform gripList;
    public Transform scopeList;
    public Transform stockList;
    public Transform magazineList;
    public Transform barrelList;
    public Transform bodyList;

    public Transform assamblePoint;
    public Text infoName;
    public Text infoText;

    public bool grip;
    public bool scope;
    public bool stock;
    public bool magazine;
    public bool barrel;
    public bool body;

    public Text damage;
    public Text fireRate;
    public Text reloadSpeed;
    public Text accuracy;
    public Text stability;
    public Text weight;
    public Text swapSpeed;
    public Text ammo;
    public WeaponPartInfo info;

    public GameObject finish;

    public bool check;

    public void Update()
    {
        if (check)
        {
            SetParts();
            check = false;
        }
    }

    public void WeaponPartSetStats()
    {
        info.damage = 0;
        info.fireRate = 0;
        info.reloadSpeed = 0;
        info.accuracy = 0;
        info.stability = 0;
        info.weight = 0;
        info.swapSpeed = 0;
        info.ammo = 0;
        foreach (Transform child in weapon.transform)
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

        damage.text = info.damage.ToString();
        fireRate.text = info.fireRate.ToString();
        reloadSpeed.text = info.reloadSpeed.ToString();
        accuracy.text = info.accuracy.ToString();
        stability.text = info.stability.ToString();
        weight.text = info.weight.ToString();
        swapSpeed.text = info.swapSpeed.ToString();
        ammo.text = info.ammo.ToString();
    }

    public void SetParts()
    {
        if (grip && stock && magazine && barrel && body)
        {
            finish.SetActive(true);
        }
        else
        {
            finish.SetActive(false);
        }
        foreach (Transform child in gripList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in scopeList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in stockList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in magazineList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in barrelList)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in bodyList)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < bodys.Length; i++)
        {
            GameObject g = Instantiate(panel, bodyList);
            AssemblerPanel p = g.GetComponent<AssemblerPanel>();
            p.inputName.text = bodys[i].name;
            p.part = bodys[i];
            p.location = assamblePoint;
            p.body = true;
            p.assembler = GetComponent<Assembler>();
            p.type = bodys[i].GetComponent<WeaponPartInfo>().type;
            p.infoName = infoName;
            p.infoText = infoText;
        }
        if (chosenBody)
        {
            BodyCompatable com = chosenBody.GetComponent<BodyCompatable>();
            for (int i = 0; i < com.grip.Length; i++)
            {
                GameObject g = Instantiate(panel, gripList);
                AssemblerPanel p = g.GetComponent<AssemblerPanel>();
                p.inputName.text = com.grip[i].name;
                p.part = com.grip[i];
                p.location = chosenBody.GetComponent<BodyAttachment>().gripPosition;
                p.assembler = GetComponent<Assembler>();
                p.grip = true;
                p.type = com.grip[i].GetComponent<WeaponPartInfo>().type;
                p.infoName = infoName;
                p.infoText = infoText;
            }
            for (int i = 0; i < com.scope.Length; i++)
            {
                GameObject g = Instantiate(panel, scopeList);
                AssemblerPanel p = g.GetComponent<AssemblerPanel>();
                p.inputName.text = com.scope[i].name;
                p.part = com.scope[i];
                p.location = chosenBody.GetComponent<BodyAttachment>().scopePosition;
                p.assembler = GetComponent<Assembler>();
                p.type = com.scope[i].GetComponent<WeaponPartInfo>().type;
                p.infoName = infoName;
                p.infoText = infoText;
            }
            for (int i = 0; i < com.stock.Length; i++)
            {
                GameObject g = Instantiate(panel, stockList);
                AssemblerPanel p = g.GetComponent<AssemblerPanel>();
                p.inputName.text = com.stock[i].name;
                p.part = com.stock[i];
                p.location = chosenBody.GetComponent<BodyAttachment>().stockPosition;
                p.assembler = GetComponent<Assembler>();
                p.type = com.stock[i].GetComponent<WeaponPartInfo>().type;
                p.infoName = infoName;
                p.infoText = infoText;
            }
            for (int i = 0; i < com.magazine.Length; i++)
            {
                GameObject g = Instantiate(panel, magazineList);
                AssemblerPanel p = g.GetComponent<AssemblerPanel>();
                p.inputName.text = com.magazine[i].name;
                p.part = com.magazine[i];
                p.location = chosenBody.GetComponent<BodyAttachment>().magazinePosition;
                p.assembler = GetComponent<Assembler>();
                p.type = com.magazine[i].GetComponent<WeaponPartInfo>().type;
                p.infoName = infoName;
                p.infoText = infoText;
            }
            if (chosenGrip)
            {
                for (int i = 0; i < com.barrel.Length; i++)
                {
                    GameObject g = Instantiate(panel, barrelList);
                    AssemblerPanel p = g.GetComponent<AssemblerPanel>();
                    p.inputName.text = com.barrel[i].name;
                    p.part = com.barrel[i];
                    p.location = chosenGrip.GetComponent<GripAttachment>().barrelPosition;
                    p.assembler = GetComponent<Assembler>();
                    p.type = com.barrel[i].GetComponent<WeaponPartInfo>().type;
                    p.infoName = infoName;
                    p.infoText = infoText;
                }
            }
        }
        if(swap.weapons.Count != 0)
        {
            startGame.SetActive(true);
        }
        WeaponPartSetStats();
    }

    public IEnumerator SecondCheck()
    {
        yield return new WaitForSeconds(0.2f);
        SetParts();
    }
}