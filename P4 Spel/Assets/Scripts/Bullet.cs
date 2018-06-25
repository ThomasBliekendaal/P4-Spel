using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10;
    public float damage;
    public Color[] possibleColors;
    private Color active;
    public TrailRenderer tR;
    public bool bouncing;

    private void Awake()
    {
        //Physics.IgnoreLayerCollision(0, 11);
        TrailRenderer tR = gameObject.GetComponent<TrailRenderer>();
        Renderer rM = gameObject.GetComponent<Renderer>();
        active = possibleColors[Random.Range(0, possibleColors.Length)];
        rM.material.color = active;
        tR.material.color = active;
        tR.material.SetColor("_EmissionColor", active);
        Destroy(gameObject, 7);
        Rigidbody rB = gameObject.GetComponent<Rigidbody>();
        rB.AddForce(10, 0, 0);
    }

    void Update()
    {
        if (!bouncing)
        {
            transform.Translate(new Vector3(0, 1, speed*2) * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(0, 0, -speed) * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
            if(collision.gameObject.tag != "Floor")
            {
                if(collision.gameObject.tag != "Splitter")
                {
                    bouncing = true;
                    Rigidbody rB = gameObject.GetComponent<Rigidbody>();
                    rB.AddForce(10, 0, 0);
                    rB.mass = 1.1f;
                    Destroy(gameObject, 0.1f);
                }
            }
        }
        if(collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject,0.4f);
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().DoDam(damage);
        }
    }
}