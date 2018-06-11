using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellLaunch : MonoBehaviour {

    public Vector3 movement;

    public void Update()
    {
        transform.Translate(movement * Time.deltaTime);
    }
}
