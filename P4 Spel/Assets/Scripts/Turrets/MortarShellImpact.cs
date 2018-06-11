using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellImpact : MonoBehaviour {

    public GameObject impact;

    public void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, -30, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject g = Instantiate(impact, transform.position, transform.rotation);
        Destroy(g, 7);
        Destroy(gameObject);
    }
}
