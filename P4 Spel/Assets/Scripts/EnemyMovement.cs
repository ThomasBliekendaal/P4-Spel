using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : HealthScript {
    [Header("Main enemy characteristics")]
    [Tooltip("The movement speed. (try to keep this balanced)")]
    public float speed;
    [Tooltip("The Amount of damage per hit. (keep it balanced)")]
    public float damage;
    [Tooltip("The end objective to which the enemy needs to walk.")]
    public GameObject chest;
    [Tooltip("When the enemy collides with the splitter collider (which should be just before the road splits into 3) the enemy takes one of the three roads.")]
    public GameObject splitter;
    [Tooltip("There should be 3 gameobjects in to which the enemy can walk after colliding with the splitter.")]
    public GameObject[] roads;

    [Header("Aggresion options")]
    [Tooltip("The players gameobject.")]
    public GameObject player;
    [Tooltip("How long the enemy stays aggresive in seconds.")]
    public float aggroTime;
    [Tooltip("Radius in which the enemies will attack.")]
    public float aggroRadius;
    [Tooltip("Time before enemies can attack again.")]
    public float aggroCooldown;

    private NavMeshAgent agent; // this is the agent component on the enemy. this will be automatically set to the correct one.
    private Vector3 currentObjective; //current objective is saved because enemies temp. chang to players.
    private bool isAggro = false; //This is true when the enemie is aggresive.
    private bool canAggro = true; //This is false during the cooldown.

	void Start () {
        player = Camera.main.gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(splitter.transform.position);
    }

    private void Awake()
    {
        player = Camera.main.gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(splitter.transform.position);
        //Temporary\\
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //Temorary\\
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= aggroRadius)
        {
            if(canAggro == true)
            {
                isAggro = true;
                StartCoroutine(StayAggro(aggroTime));
            }
        }

        if(isAggro == true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > aggroRadius)
            {
                canAggro = false;
                isAggro = false;
                StartCoroutine(AggroCooldown(aggroCooldown));
            }
        }

        if(isAggro == true)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(currentObjective);
        }

    }

    public IEnumerator StayAggro(float aggTime)
    {
        yield return new WaitForSeconds(aggTime);
        isAggro = false;
        canAggro = false;
        StartCoroutine(AggroCooldown(aggroCooldown));
    }

    public IEnumerator AggroCooldown(float aggroCooldown)
    {
        yield return new WaitForSeconds(aggroCooldown);
        canAggro = true;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == splitter) //this checks if the object the enemies collide with is the splitter.
        {
            agent.SetDestination(roads[Random.Range(0, 3)].transform.position);
            currentObjective = agent.destination;
        }
        if (col.gameObject.tag == ("RoadCollider")) //checks if the enemy collides with one of the road points which should set their destinations to the end.
        {
            agent.SetDestination(chest.transform.position);
            currentObjective = agent.destination;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, gameObject.GetComponent<Collider>());
        }
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
    }
}
