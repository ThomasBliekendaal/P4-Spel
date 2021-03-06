﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour {

    public float firerate;
    public float speed;
    public float damage;
    public Element type;
    public GameObject electric;
    public GameObject electricType;
    public GameObject damagePopUp;
    public bool turret;

    public void Start()
    {
        if (type == Element.electric)
        {
            electricType.SetActive(true);
        }
    }

    public void Update()
    {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(type == Element.electric)
            {
                GameObject g = Instantiate(electric, transform.position, transform.rotation);
                g.GetComponent<ElectricBeam>().target = collision.gameObject.transform;
                g.GetComponent<ElectricBeam>().damage = damage / 2;
            }
            if (!turret)
            {
                GameObject d = Instantiate(damagePopUp, transform.position, Quaternion.identity);
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>().PlaySound(0);
                d.GetComponent<DamagePopUp>().damage = Mathf.CeilToInt(damage);
                d.GetComponent<Rigidbody>().velocity += (new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)));
                Destroy(d, 0.5f);
            }
            collision.gameObject.GetComponent<EnemyMovement>().DoDam(damage);
        }
        Destroy(gameObject);
    }
}
