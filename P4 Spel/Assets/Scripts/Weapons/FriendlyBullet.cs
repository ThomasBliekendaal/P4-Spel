using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBullet : MonoBehaviour {

    public float firerate;
    public float speed;
    public float damage;
    public Element type;
    public GameObject electric;
    public GameObject electricType;

    public void Start()
    {
        if (type == Element.electric)
        {
            electricType.SetActive(true);
        }
    }

    public void Update()
    {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(type == Element.electric)
            {
                GameObject g = Instantiate(electric, transform.position, transform.rotation);
                g.GetComponent<ElectricBeam>().target = collision.gameObject.transform;
                g.GetComponent<ElectricBeam>().damage = damage / 2;
            }
            collision.gameObject.GetComponent<EnemyMovement>().DoDam(damage);
            print("HIT");
        }
        Destroy(gameObject);
    }
}
