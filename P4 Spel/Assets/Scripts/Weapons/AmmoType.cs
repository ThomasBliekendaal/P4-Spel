using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmmoType", menuName = "AmmoType")]
public class AmmoType : ScriptableObject
{
    public enum Type { kinetic, explosive, piercing }
    public Type ammoType;
    public GameObject projectile;
}
