using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public int wave = 0;
    public int amountOfWaves;

    public GameObject basicMelee;
    public GameObject basicRanged;
    public GameObject basicTank;
    public GameObject suicideBomber;
    public GameObject buffTank;
    public GameObject huntingArty;

    public GameObject[] spawnLocations;
    public float radius = 3;

    public int mA = 7;
    public int rA = 5;
    public int tA;

    //debug
    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < mA; i++)
        {
            GameObject e = Instantiate(basicMelee, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0,spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "bmelee" + i;
        }
        for (int i = 0; i < rA; i++)
        {
            GameObject e = Instantiate(basicRanged, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "branged" + i;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            mA += 3;
            rA += 2;
            tA += 1;
            NextWave();
        }
	}

    public void Arty()
    {
        print("An Arty has spawned!");
    }

    public void Bomber()
    {
        print("A bomber has spawned!");
    }

    public void Tank()
    {
        print("A tank has spawned!");
    }

    public void NextWave()
    {
        wave++;
        print("WAVE" + wave);
        for (int i = 0; i < mA; i++)
        {
            GameObject e = Instantiate(basicMelee, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "bmelee" + i;
        }
        for (int i = 0; i < rA; i++)
        {
            GameObject e = Instantiate(basicRanged, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "branged" + i;
        }
        for (int i = 0; i < tA; i++)
        {
            GameObject e = Instantiate(basicTank, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "btank" + i;
        }
    }

    public void SpecialEventArty() // 50%
    {
        if (wave >= amountOfWaves - amountOfWaves / 2)
        {
            if (Random.Range(0, 1) == 0)
            {
                GameObject e = Instantiate(huntingArty, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
                e.gameObject.name = "Arty";
                e.GetComponent<Material>().color = Color.cyan;
                Arty();
            }
        }
    }

    public void SpecialEventBuffTank() // 25% t/4
    {
        if (wave >= amountOfWaves - amountOfWaves / 4)
        {
            if (Random.Range(0, 3) == 0)
            {
                GameObject e = Instantiate(buffTank, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
                e.gameObject.name = "Buff";
                e.GetComponent<Material>().color = Color.red;
                Tank();
            }
        }
    }

    public void SpecialEventBomber() // 75% t/2
    {
        if(wave>= amountOfWaves / 4)
        {
            if (Random.Range(0, 3) != 0)
            {
                GameObject e = Instantiate(suicideBomber, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
                e.gameObject.name = "Bomber";
                e.GetComponent<Material>().color = Color.black;
                Bomber();
            }
        }
    }
}