using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPaths : MonoBehaviour {
    [Header("Trigger Values")]
    [Tooltip("Put all the possible paths for this trigger here.")]
    public GameObject[] paths;

    [Header("Put this script on the paths too")]
    private GameObject infoAbove;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            NavMeshAgent agent = col.gameObject.GetComponent<EnemyMovement>().agent;
            agent.SetDestination(paths[Random.Range(0, paths.Length)].transform.position);
            col.gameObject.GetComponent<EnemyMovement>().currentObjective = col.gameObject.GetComponent<EnemyMovement>().agent.destination;
        }
    }
}
