using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRocket : MonoBehaviour {
    public float damage;
    public float fireRate;
    public float speed;
    public GameObject explosion;
    public float range;
    public LayerMask mask;
    public Element element;
    public GameObject elec;
    public GameObject damagePopUp;

    public void Update()
    {
        transform.Translate(new Vector3(0,0,speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    public void Explosion()
    {
        if(element == Element.electric)
        {
            GameObject g = Instantiate(elec, transform.position + -transform.forward, transform.rotation);
            g.GetComponent<ElectricityBall>().damage = damage / 2;
            g.GetComponent<ElectricityBall>().fireRate = 1/fireRate;
            g.GetComponent<ElectricityBall>().lifeTime = 3 /(24 / damage);
            Destroy(gameObject);
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Enemy")
                {
                    if(damage / Vector3.Distance(transform.position, colliders[i].transform.position) <= damage)
                    {
                        colliders[i].GetComponent<EnemyMovement>().DoDam(damage / Vector3.Distance(transform.position, colliders[i].transform.position));
                        GameObject d = Instantiate(damagePopUp, colliders[i].transform.position, Quaternion.identity);
                        d.GetComponent<DamagePopUp>().damage = Mathf.CeilToInt(damage / Vector3.Distance(transform.position, colliders[i].transform.position));
                        d.GetComponent<Rigidbody>().velocity += (new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)));
                        Destroy(d, 0.5f);
                    }
                    else
                    {
                        colliders[i].GetComponent<EnemyMovement>().DoDam(damage);
                        GameObject d = Instantiate(damagePopUp, colliders[i].transform.position, Quaternion.identity);
                        d.GetComponent<DamagePopUp>().damage = Mathf.CeilToInt(damage);
                        d.GetComponent<Rigidbody>().velocity += (new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)));
                        Destroy(d, 0.5f);
                    }
                    
                }
            }
            GameObject g2 = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(g2, 4);
        }
        else
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].tag == "Enemy")
                {
                    if (damage / Vector3.Distance(transform.position, colliders[i].transform.position) <= damage)
                    {
                        colliders[i].GetComponent<EnemyMovement>().DoDam(damage / Vector3.Distance(transform.position, colliders[i].transform.position));
                        GameObject d = Instantiate(damagePopUp, colliders[i].transform.position, Quaternion.identity);
                        d.GetComponent<DamagePopUp>().damage = Mathf.CeilToInt(damage / Vector3.Distance(transform.position, colliders[i].transform.position));
                        d.GetComponent<Rigidbody>().velocity += (new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)));
                        Destroy(d, 0.5f);
                    }
                    else
                    {
                        colliders[i].GetComponent<EnemyMovement>().DoDam(damage);
                        GameObject d = Instantiate(damagePopUp, colliders[i].transform.position, Quaternion.identity);
                        d.GetComponent<DamagePopUp>().damage = Mathf.CeilToInt(damage);
                        d.GetComponent<Rigidbody>().velocity += (new Vector3(Random.Range(-2, 2), 4, Random.Range(-2, 2)));
                        Destroy(d, 0.5f);
                    }
                }
            }
            GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(g, 4);
            Destroy(gameObject);
        }
    }
}
