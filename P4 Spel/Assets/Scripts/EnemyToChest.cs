using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyToChest : MonoBehaviour {

    public Transform t;
    public Vector3 v;
    public bool isArcher;
    private NavMeshAgent agent;
    private GameObject player;
    public float shootingDistance;
    public bool ranged = false;
    public float attackSpeed;
    public float meleeSpeed;
    public bool waiter;
    public GameObject bullet;
    public GameObject shooter;
    public bool ready;
    public bool spamPrevent = false;
    public bool hasHit;
    public float meleeDam;
    public bool attacking;
    public GameObject mainObjective;
    public float rangedMovingSpeed;
    public float width;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("ShootingTimer", 1, attackSpeed);
        InvokeRepeating("MeleeTimer", 1, meleeSpeed);
    }
	
	// Update is called once per frame
	void Update () {
        t = FindClosestEnemy().transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = t.position;
        if (!isArcher)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            if (mainObjective != null)
            {
                t = mainObjective.transform;
                agent.destination = t.position;
                return;
            }
        }
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        agent.speed = rangedMovingSpeed;
        v = new Vector3(transform.position.x, transform.position.y, (transform.position.z+5));
        RaycastHit hit;
        if (mainObjective != null)
        {
            t = mainObjective.transform;
            agent.destination = t.position;
            return;
        }
        if (Physics.SphereCast(transform.position, width, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.name == "Player" && t)
            {
                player = hit.transform.gameObject;
                if (Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)
                {
                    agent.isStopped = true;
                    ranged = true;
                }
            }
            else
            {
                agent.isStopped = false;
                ranged = false;
            }
        }
        shooter.transform.LookAt(t);
    }

    public void ShootingTimer()
    {
        if (ranged == true)
        {
            Shoot();
        }
    }

    public void MeleeTimer()
    {
        attacking = true;
    }

    public void Shoot()
    {
        GameObject thing = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
        thing.gameObject.GetComponent<Bullet>().host = shooter;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ally")
        {
            if(collision.gameObject.name == "Char")
            {
                collision.gameObject.GetComponent<Dissulf>().Disintegrate();
                collision.gameObject.tag = "Untagged";
                return;
            }
            if (attacking)
            {
                collision.gameObject.GetComponent<Health>().DoDam(meleeDam/2);
                attacking = false;
            }
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ally")
        {
            if (attacking)
            {
                collision.gameObject.GetComponent<Health>().DoDam(meleeDam);
                attacking = false;
            }
        }
    }
}
