﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour {

    public Transform barrelRotation;
    public Transform rotation;
    public Transform bulletPosition;
    public GameObject bullet;
    public float spread;
    public float range;
    public float damage;
    public float fireRate;
    public bool active;
    public List<Transform> enemy;

    public bool activeFire;

    public void Update()
    {
        rotation.Rotate(10, 0, 0);
        if (active)
        {
            enemy = new List<Transform>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    enemy.Add(colliders[i].transform);
                }
            }

            if(enemy.Count > 0)
            {
                transform.LookAt(new Vector3(enemy[0].position.x, transform.position.y, enemy[0].position.z));
                barrelRotation.LookAt(enemy[0].position);
                if (!activeFire)
                {
                    activeFire = true;
                    StartCoroutine(Fire());
                }
            }
            else
            {
                activeFire = false;
            }
        }
    }

    public IEnumerator Fire()
    {
        if (activeFire)
        {
            GameObject g = Instantiate(bullet, bulletPosition.position, bulletPosition.rotation);
            g.GetComponent<FriendlyBullet>().damage = damage;
            g.transform.Rotate(Random.Range(-spread,spread), Random.Range(-spread, spread), Random.Range(-spread, spread));
            Destroy(g, 3);
        }
        yield return new WaitForSeconds(4/fireRate);
        if (activeFire)
        {
            StartCoroutine(Fire());
        }
    }
}
