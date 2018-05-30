using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporarySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float respawnRate;
    public float radius;
    public float amount;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Instantatior", respawnRate, respawnRate);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Instantatior()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(enemyPrefab, Random.insideUnitSphere * radius + transform.position, Random.rotation);
        }       
    }
}
