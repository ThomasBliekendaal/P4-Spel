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

    public float timeBetweenWaves = 5;
    public bool timerStarted;

    public GameObject[] spawnLocations;
    public float radius = 3;

    public int mA = 1;
    public int rA = 3;
    public int tA;

    public GameObject[] portalRotatorz;

    //debug
    public GameObject[] enemies;

    // Use this for initialization
    void Start () {
        StartCoroutine(NextWave());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length < 1)
        {
            if (timerStarted == false)
            {
                timerStarted = true;
                StartCoroutine(TillNextWave(timeBetweenWaves));
                mA += 3;
                rA += 2;
                tA += 1;
            }
        }
	}

    public IEnumerator TillNextWave(float timeToNextWave)
    {
        yield return new WaitForSeconds(timeToNextWave);
        StartCoroutine(NextWave());
        timerStarted = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
        for (int i = 0; i < portalRotatorz.Length; i++)
        {
            portalRotatorz[i].transform.Rotate(0, 50 * Time.deltaTime, 0);
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

    public IEnumerator NextWave()
    {
        wave++;
        print("WAVE" + wave);
        for (int i = 0; i < mA; i++)
        {
            GameObject e = Instantiate(basicMelee, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "bmelee" + i;
            yield return new WaitForSeconds(1.1f);
        }
        for (int i = 0; i < rA; i++)
        {
            GameObject e = Instantiate(basicRanged, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "branged" + i;
            yield return new WaitForSeconds(1.1f);
        }
        for (int i = 0; i < tA; i++)
        {
            GameObject e = Instantiate(basicTank, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
            e.gameObject.name = "btank" + i;
            yield return new WaitForSeconds(1.1f);
        }

        SpecialEventArty();
        SpecialEventBomber();
        SpecialEventBuffTank();
    }

    public void SpecialEventArty() // 50%
    {
        if (wave >= amountOfWaves - amountOfWaves / 2)
        {
            if (Random.Range(0, 1) == 0)
            {
                GameObject e = Instantiate(huntingArty, Random.insideUnitSphere * radius + spawnLocations[Random.Range(0, spawnLocations.Length)].transform.position, Random.rotation);
                e.gameObject.name = "Arty";
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
                Bomber();
            }
        }
    }
}