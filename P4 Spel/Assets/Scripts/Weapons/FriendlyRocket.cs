using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRocket : MonoBehaviour {
    public float damage;
    public float speed;
    public GameObject explosion;
    public float range;
    public LayerMask mask;

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].tag == "Enemy")
            {
                colliders[i].GetComponent<EnemyMovement>().DoDam(damage / Vector3.Distance(transform.position,colliders[i].transform.position));
            }
        }
        GameObject g = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(g, 4);
        Destroy(gameObject);
    }
}
