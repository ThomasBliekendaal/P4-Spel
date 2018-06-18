using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour {

    public List<GameObject> weapons;
    public int current;


    public void Update()
    {
        
    }

    public void Switch(int add)
    {
        current += add;
        if(current > weapons.Count)
        {
            current = 0;
        }
        else if(current <= 0)
        {
            
        }
    }
}
