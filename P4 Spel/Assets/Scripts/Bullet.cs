using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 40;
    public float damage;

    void Start () {
        
    }
	
	void Update () {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 5f);
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthScript>().DoDam(damage);
        }
        if(collision.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}
