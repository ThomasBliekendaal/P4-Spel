using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblerPanel : MonoBehaviour
{

    public Text inputName;
    public GameObject part;
    public Transform location;
    public bool body;
    public bool grip;
    public Assembler assembler;
    public PartType type;

    public void OnButtonPress()
    {
        foreach (Transform child in location)
        {
            if (child.GetComponent<WeaponPartInfo>().type == type)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject g = Instantiate(part, location.position, location.rotation, location);
        if (type == PartType.barrel)
        {
            assembler.barrel = true;
        }
        if (type == PartType.body)
        {
            assembler.body = true;
            assembler.barrel = false;
            assembler.grip = false;
            assembler.scope = false;
            assembler.stock = false;
            assembler.magazine = false;
        }
        if (type == PartType.stock)
        {
            assembler.stock = true;
        }
        if (type == PartType.grip)
        {
            assembler.grip = true;
            assembler.barrel = false;
        }
        if (type == PartType.scope)
        {
            assembler.scope = true;
        }
        if (body)
        {
            assembler.chosenBody = g;
        }
        if (grip)
        {
            assembler.chosenGrip = g;
        }
        if (type == PartType.magazine)
        {
            assembler.magazine = true;
        }
        assembler.SetParts();
    }
}
