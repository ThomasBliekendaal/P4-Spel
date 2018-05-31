using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour {
    public float speed;
    public float damage;

    public void Update()
    {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyMovement>().DoDam(damage);
        }
    }
}
