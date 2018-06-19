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
    public float fireRate;
    public LayerMask mask;

    public void Update()
    {
        enemy = new List<Transform>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
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
                StartCoroutine(ShootTimer(enemy[0], enemy[0].position));
                active = true;
            }
        }
    }

    public IEnumerator ShootTimer(Transform position, Vector3 otherPos)
    {
        yield return new WaitForSeconds(fireRate);
        if(position != null)
        {
            StartCoroutine(Fire(position, otherPos));
        }
        else
        {
            StartCoroutine(Fire(null, otherPos));
        }
    }

    public IEnumerator Fire(Transform target, Vector3 pos)
    {
        GameObject g = Instantiate(shellStart, firePoint.position, firePoint.rotation);
        active = false;
        yield return new WaitForSeconds(5);
        Destroy(g);
        if(target != null)
        {
            Impact(target.position);
        }
        else
        {
            Impact(pos);
        }
    }

    public void Impact(Vector3 position)
    {
        Instantiate(shellImpact, position + Vector3.up * 90, Quaternion.identity);
    }
}
