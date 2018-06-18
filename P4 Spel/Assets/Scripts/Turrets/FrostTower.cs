using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostTower : MonoBehaviour {

    public float range;
    public float slowTime;
    public float slowStrenght;

    public List<GameObject> enemies;
    public List<GameObject> slowed;

    public bool active;
    public GameObject particle;

    public void Start()
    {
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(1.2f);
        active = true;
        particle.SetActive(true);
    }

    public void Update()
    {
        if (active)
        {
            enemies = new List<GameObject>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    enemies.Add(colliders[i].gameObject);
                    StartCoroutine(Slow(colliders[i].gameObject));
                }
            }
        }
    }

    public IEnumerator Slow(GameObject enemy)
    {
        bool b = true;
        for (int i = 0; i < slowed.Count; i++)
        {
            if(enemy == slowed[i])
            {
                b = false;
            }
        }
        if (b)
        {
            slowed.Add(enemy);
            enemy.GetComponent<EnemyMovement>().speed /= slowStrenght;
            yield return new WaitForSeconds(slowTime);
            if(enemy != null)
            {
                enemy.GetComponent<EnemyMovement>().speed *= slowStrenght;
            }
        }
    }
}
