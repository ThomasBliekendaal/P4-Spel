using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDeploy : MonoBehaviour
{

    public GameObject weapon;
    public GameObject activeWeapon;
    public GameObject rotationPoint;
    public bool active;
    public List<Transform> Enemy;
    public float range;
    public WeaponPartInfo info;
    public Vector3 r;

    public bool activeFire;
    public RaycastHit hit;
    public LayerMask mask;

    public void Start()
    {
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(2);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        GameObject g = Instantiate(weapon, rotationPoint.transform.position, transform.rotation, rotationPoint.transform);
        rotationPoint.transform.rotation = new Quaternion(0, transform.rotation.y, 0, rotationPoint.transform.rotation.w);
        activeWeapon = g;
        Destroy(g.GetComponent<Weapon>());
        info = activeWeapon.GetComponent<WeaponPartInfo>();
        active = true;
    }

    public void Update()
    {
        Enemy = new List<Transform>();
        if (active)
        {
            activeWeapon.transform.position = Vector3.MoveTowards(activeWeapon.transform.position, rotationPoint.transform.position, Time.deltaTime);
            Collider[] colliders = Physics.OverlapSphere(transform.position, range, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.tag == "Enemy")
                {
                    if (Physics.Raycast(transform.position, (colliders[i].transform.position - transform.position).normalized, out hit, range) && hit.transform.gameObject == colliders[i].gameObject)
                    {
                        Enemy.Add(colliders[i].transform);
                    }
                }
            }

            if (Enemy.Count != 0)
            {
                if (!activeFire)
                {
                    activeFire = true;
                    StartCoroutine(Fire());
                }
                rotationPoint.transform.LookAt(Enemy[0]);
            }
            else if (Enemy.Count == 0)
            {
                activeFire = false;
            }
        }
    }

    public IEnumerator Fire()
    {
        r = new Vector3(Random.Range(5 / -info.accuracy, 5 / info.accuracy), Random.Range(2 / -info.accuracy, 2 / info.accuracy), Random.Range(2 / -info.accuracy, 2 / info.accuracy));
        activeWeapon.transform.Translate(0, 1 / info.stability, 0);
        GameObject g = Instantiate(info.ammoType.projectile, rotationPoint.transform.position, rotationPoint.transform.rotation);
        g.transform.Rotate(r);
        g.GetComponent<FriendlyBullet>().type = activeWeapon.GetComponent<GripType>().element;
        g.GetComponent<FriendlyBullet>().damage = info.damage;
        g.GetComponent<FriendlyBullet>().firerate = info.fireRate;
        Destroy(g, 5);
        yield return new WaitForSeconds(1 / info.fireRate);
        if (activeFire)
        {
            StartCoroutine(Fire());
        }
    }
}
