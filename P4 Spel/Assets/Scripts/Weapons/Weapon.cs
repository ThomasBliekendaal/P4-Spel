using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public WeaponPartInfo info;
    public float standardSpeed;
    public bool active;
    public Transform barrel;
    private bool allow;
    public Vector3 r;

    public void OnEnable()
    {
        StartCoroutine(StartTimer());
    }

    public void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (active == false)
            {
                StartCoroutine(Fire2());
                active = true;
            }
        }
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForEndOfFrame();
        foreach (Transform child1 in transform)
        {
            foreach (Transform child2 in child1)
            {
                foreach (Transform child3 in child2)
                {
                    foreach (Transform child4 in child3)
                    {
                        foreach (Transform child5 in child4)
                        {
                            if (child5.GetComponent<WeaponPartInfo>().type == PartType.barrel)
                            {
                                barrel = child5.transform;
                            }
                        }
                    }
                }
            }
        }
    }

    public IEnumerator Fire2()
    {
        if (active)
        {
            r = new Vector3(Random.Range(-info.accuracy / 2, info.accuracy / 2), Random.Range(-info.accuracy, info.accuracy) * 90, Random.Range(-info.accuracy, info.accuracy) * 7);
            GameObject g = Instantiate(info.ammoType.projectile, barrel.position, barrel.rotation);
            g.GetComponent<Bullet>().host = transform.parent.parent.parent.gameObject;
            g.transform.Rotate(r);
            Destroy(g, 2);
            transform.parent.parent.Rotate(new Vector3(-1 / info.stability, 0, 0) * 10);
            transform.parent.parent.parent.Rotate(new Vector3(0, Random.Range(-1 / info.stability, 1 / info.stability), 0) * 10);
        }
        yield return new WaitForSeconds(1 / info.fireRate);
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(Fire2());
        }
        else
        {
            active = false;
        }
    }
}
