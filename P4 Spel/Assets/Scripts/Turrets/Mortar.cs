using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : MonoBehaviour {
    public float range;
    public Transform barrel;
    public Transform firePoint;
    public List<Transform> enemy;
    public GameObject shellStart;
    public GameObject shellImpact;
    public bool active;

    public void Update()
    {
        enemy = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Enemy")
            {
                enemy.Add(colliders[i].transform);
            }
        }

        if(enemy.Count > 0)
        {
            transform.LookAt(new Vector3(enemy[0].position.x, transform.position.y, enemy[0].position.z));
            barrel.LookAt(enemy[0].position + Vector3.up * 150);
            if (!active)
            {
                StartCoroutine(ShootTimer(enemy[0]));
                active = true;
            }
        }
    }

    public IEnumerator ShootTimer(Transform position)
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Fire(position.position));
    }

    public IEnumerator Fire(Vector3 target)
    {
        GameObject g = Instantiate(shellStart, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(3);
        Destroy(g);
        Impact(target);
    }

    public void Impact(Vector3 position)
    {
        GameObject g = Instantiate(shellImpact, position + Vector3.up * 50, Quaternion.identity);
        active = false;
    }
}
