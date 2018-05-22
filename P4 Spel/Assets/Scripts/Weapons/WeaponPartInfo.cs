using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPartInfo : MonoBehaviour
{

    public float damage;
    public float fireRate;
    public float reloadSpeed;
    public float accuracy;
    public float stability;
    public float weight;
    public float swapSpeed;
    public int ammo;
    public PartType type;
    public AmmoType ammoType;
}

[System.Serializable]
public enum PartType { body, barrel, scope, magazine, stock, grip }
