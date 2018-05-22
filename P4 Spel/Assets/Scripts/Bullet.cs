using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public GameObject host;
    public float damage;
    
	void Start () {
        transform.rotation = host.transform.rotation;
    }
	
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
	}

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 5f);
        if(collision.gameObject.tag == "Ally")
        {
            collision.gameObject.GetComponent<HealthScript>().DoDam(damage);
        }
        if(collision.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}
