﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRocket : MonoBehaviour {
    public float damage;
    public float fireRate;
    public float speed;
    public GameObject explosion;
    public float range;
    public LayerMask mask;
    public Element element;
    public GameObject elec;

    public void Update()
    {
        transform.Translate(new Vector3(0,0,speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    public void Explosion()
    {
        if(element == Element.electric)
        {
            GameObject g = Instantiate(elec, transform.position, transform.rotation);
            g.GetComponent<ElectricityBall>().damage = damage / 2;
            g.GetComponent<ElectricityBall>().fireRate = fireRate;
            g.GetComponent<ElectricityBall>().lifeTime = 3 /(24 / damage);
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Enemy")
                {
                    colliders[i].GetComponent<EnemyMovement>().DoDam(damage / Vector3.Distance(transform.position, colliders[i].transform.position/2));
                }
            }
            GameObject g2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(g2, 4);
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Enemy")
                {
                    colliders[i].GetComponent<EnemyMovement>().DoDam(damage / Vector3.Distance(transform.position, colliders[i].transform.position));
                }
            }
            GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(g, 4);
            Destroy(gameObject);
        }
    }
}
