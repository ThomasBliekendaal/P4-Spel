﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBeam : MonoBehaviour
{

    public int checkAmount;
    public float range;
    public Transform target;
    public float speed;
    public bool active;
    public List<Transform> targets;
    public float damage;

    public void Start()
    {

    }

    public void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (active)
            {
                if (transform.position == target.position)
                {
                    CheckForEnemy();
                    target.GetComponent<HealthScript>().DoDam(damage);
                }
            }
        }
        else
        {
            CheckForEnemy();
        }
    }

    public void CheckForEnemy()
    {
        targets = new List<Transform>();
        if (checkAmount > 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (target == null)
                    {
                        targets.Add(colliders[i].transform);
                    }
                    else if (colliders[i].gameObject != target.gameObject)
                    {
                        targets.Add(colliders[i].transform);
                    }
                }
            }
            if(targets.Count == 0)
            {
                active = false;
                Destroy(gameObject, 0.3f);
            }
            else
            {
                int bleh = Random.Range(0, targets.Count);
                target = targets[bleh];
                checkAmount--;
            }
        }
        else
        {
            active = false;
            Destroy(gameObject, 0.3f);
        }
    }
}