using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDeploy : MonoBehaviour {

    public GameObject impact;
    public bool falling;
    public GameObject activeTurret;
    public float distance;
    public Material on;
    public Transform rotation;
    public GameObject cam;
    public float camDistance;
    public GameObject ui;
    public GameObject minimap;
    public Material used;


    void Update () {
        camDistance = Vector3.Distance(transform.position, cam.transform.parent.position);
        if(camDistance <= 5 && !activeTurret)
        {
            rotation.gameObject.SetActive(true);
            rotation.LookAt(cam.transform);
            if (Input.GetButtonDown("Interact"))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                ui.SetActive(true);
                ui.GetComponent<TurretUI>().location = gameObject;
            }
        }
        else if(!activeTurret)
        {
            rotation.gameObject.SetActive(false);
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

    public void SetTurret(GameObject turret)
    {
        rotation.gameObject.SetActive(false);
        GetComponent<Renderer>().material = on;
        activeTurret = Instantiate(turret, transform.position + Vector3.up * 30,Quaternion.identity);
        falling = true;
        minimap.GetComponent<Renderer>().material = used;
    }
}
