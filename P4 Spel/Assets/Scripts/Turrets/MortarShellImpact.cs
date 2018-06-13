using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarShellImpact : MonoBehaviour {

    public GameObject impact;
    public ParticleSystem particle;
    public GameObject shell;
    public float damage;
    public GameObject impactGround;

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, 7);
        GameObject g2 = Instantiate(impactGround, transform.position + Vector3.up * 0.2f, Quaternion.identity);
        Destroy(g2, 3);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                colliders[i].GetComponent<HealthScript>().DoDam(damage *  (1/ Vector3.Distance(transform.position, colliders[i].transform.position)));
            }
        }
    }
}
