using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellImpact : MonoBehaviour {

    public GameObject impact;
    public ParticleSystem particle;
    public GameObject shell;

    public void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity += new Vector3(0, -70, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject g = Instantiate(impact, transform.position, Quaternion.identity);
        GetComponent<Collider>().isTrigger = true;
        particle.Stop();
        Destroy(g, 7);
        shell.SetActive(false);
        Destroy(gameObject, 3);
    }
}
