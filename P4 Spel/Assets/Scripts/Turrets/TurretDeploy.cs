using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeploy : MonoBehaviour {

    public GameObject impact;
    public GameObject turret;
    public bool falling;
    public bool active;
    public GameObject activeTurret;
    public float distance;
    public Material on;

	void Update () {
        if (active)
        {
            active = false;
            SetTurret();
        }
        if (falling)
        {
            distance = Vector3.Distance(transform.position + (Vector3.up * 0.9f), activeTurret.transform.position);
            activeTurret.transform.Translate(-Vector3.up * 20 * Time.deltaTime);
            if(distance <= 0.5f)
            {
                GameObject g = Instantiate(impact, transform.position + (Vector3.up * 0.9f), Quaternion.identity);
                    Destroy(g, 3);
                falling = false;
                activeTurret.transform.position = transform.position + (Vector3.up * 0.9f);
            }
        }
	}

    public void SetTurret()
    {
        GetComponent<Renderer>().material = on;
        activeTurret = Instantiate(turret, transform.position + Vector3.up * 30,Quaternion.identity);
        falling = true;
    }
}
