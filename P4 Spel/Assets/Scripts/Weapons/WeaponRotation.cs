using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour {
    public bool active;
    public bool active2;
    public float speed;

	public void OnHoverEnter()
    {
        active = true;
    }

    public void OnHoverExit()
    {
        active = false;
    }

    public void Update()
    {
        if (Input.GetButtonDown("Fire1") && active)
        {
            active2 = true;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            active2 = false;
        }
        if (Input.GetButton("Fire1") && active2)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * speed, Space.World);
        }
    }
}
