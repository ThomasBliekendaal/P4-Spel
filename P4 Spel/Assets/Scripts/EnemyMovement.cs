using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : HealthScript
{
    [Header("Main enemy characteristics")]
    [Tooltip("The movement speed. (try to keep this balanced)")]
    public float speed;
    [Tooltip("The Amount of damage per hit. (keep it balanced)")]
    public float damage;
    [Tooltip("When the enemy collides with the splitter collider (which should be just before the road splits into 3) the enemy takes one of the three roads.")]
    public GameObject splitter;

    [Header("Aggresion options")]
    [Tooltip("The players gameobject.")]
    public GameObject player;
    [Tooltip("How long the enemy stays aggresive in seconds.")]
    public float aggroTime;
    [Tooltip("Radius in which the enemies will attack.")]
    public float aggroRadius;
    [Tooltip("Time before enemies can attack again.")]
    public float aggroCooldown;

    public NavMeshAgent agent; // this is the agent component on the enemy. this will be automatically set to the correct one.
    public Vector3 currentObjective; //current objective is saved because enemies temp. chang to players.
    public bool isAggro = false; //This is true when the enemie is aggresive.
    private bool canAggro = true; //This is false during the cooldown.
    private bool dying;

    public ParticleSystem dyingAnimation;

    void Start()
    {
        player = Camera.main.gameObject;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(splitter.transform.position);
        currentObjective = splitter.transform.position;
        health = maxHealth;
    }

    private void Awake()
    {
        if (!Camera.main)
        {
            return;
        }
        player = Camera.main.gameObject;
        splitter = GameObject.FindGameObjectWithTag("Splitter");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(splitter.transform.position);
        currentObjective = splitter.transform.position;
        health = maxHealth;
    }

    void Update()
    {
        if(player == null)
        {
            return;
        }
        agent.speed = speed;
        if (health <= 0)
        {
            dying = true;
            Destroy(gameObject,0.7f);
        }

        if (dying == true)
        {
            transform.Translate(0, -1f * Time.deltaTime, 0);
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            if (dyingAnimation)
            {
                dyingAnimation.Play();
            }
            return;
        }

        if(gameObject.transform.position.y < 1)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= aggroRadius)
        {
            if (canAggro == true)
            {
                isAggro = true;
                StartCoroutine(StayAggro(aggroTime));
            }
        }

        if (isAggro == true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > aggroRadius)
            {
                canAggro = false;
                isAggro = false;
                StartCoroutine(AggroCooldown(aggroCooldown));
            }
        }

        if (isAggro == true)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(currentObjective);
        }

        if (gameObject.GetComponent<EnemyAttackBase>().attackType == EnemyAttackBase.State2.Runner)
        {
            agent.SetDestination(Camera.main.transform.position);
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.GetComponent<EnemyAttackBase>().attackType == EnemyAttackBase.State2.Runner)
            {
                Destroy(gameObject, 1f);
                gameObject.GetComponent<EnemyAttackBase>().pS.Play();
                gameObject.GetComponent<EnemyAttackBase>().enabled = false;
                gameObject.GetComponent<EnemyMovement>().enabled = false;
                collision.gameObject.GetComponent<Rigidbody>().velocity += (transform.forward + transform.up) * 10;
            }
        }
    }

    public void RemoteControl(Transform target)
    {
        agent.SetDestination(target.position);
    }
}