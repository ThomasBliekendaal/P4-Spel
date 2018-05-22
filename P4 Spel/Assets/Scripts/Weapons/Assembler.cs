using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : MonoBehaviour
{

    public GameObject[] bodys;
    public GameObject chosenBody;
    public GameObject chosenGrip;

    public GameObject panel;

    public Transform gripList;
    public Transform scopeList;
    public Transform stockList;
    public Transform magazineList;
    public Transform barrelList;
    public Transform bodyList;

    public Transform assamblePoint;

    public bool grip;
    public bool scope;
    public bool stock;
    public bool magazine;
    public bool barrel;
    public bool body;

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
                }
            }
        }
    }
}
