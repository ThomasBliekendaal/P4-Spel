﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;
    public float damage;

    void Start () {
        
    }
	
	void Update () {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 5f);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().DoDam(damage);
        }
        if(collision.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(gameObject.gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }
    }
}
