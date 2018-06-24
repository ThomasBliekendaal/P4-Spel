using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityBall : MonoBehaviour {

    public Transform target;
    public GameObject beam;
    public float range;
    public LayerMask mask;
    public float fireRate;
    public float damage;
    public bool activeFire;
    [Range(0.5f,3)]
    public float lifeTime;

    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Update()
    {
        if (!target)
        {
            RaycastHit hit;
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (Physics.Raycast(transform.position, (colliders[i].transform.position - transform.position).normalized, out hit, range) && hit.transform.gameObject == colliders[i].gameObject)
                    {
                        target = colliders[i].transform;
                        break;
                    }
                }
            }
        }
        else if(!activeFire)
        {
            StartCoroutine(Fire());
        }
    }

    public IEnumerator Fire()
    {
        activeFire = true;
        GameObject g = Instantiate(beam, transform.position, transform.rotation);
        ElectricBeam elec = g.GetComponent<ElectricBeam>();
        elec.damage = damage;
        elec.target = target;
        elec.checkAmount = 0;

        yield return new WaitForSeconds(fireRate);
        activeFire = false;
    }
}
