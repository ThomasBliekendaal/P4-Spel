using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{

    public WeaponPartInfo info;
    public float standardSpeed;
    public bool active;
    public Transform barrel;
    private bool allow;
    public Vector3 r;
    public Transform otherPos;
    public RaycastHit hit;
    public Quaternion rot;
    public int currentAmmo;
    public bool activeReload;
    public Text ammoInput;
    public Image ammoVisual;
    public bool sc;
    public bool dontCheck;
    public bool openMenuWeap;

    public void OnEnable()
    {
        if (!dontCheck)
        {
            StartCoroutine(StartTimer());
        }
    }

    public void Update()
    {
        if (!openMenuWeap)
        {
            transform.parent.parent.parent.GetComponent<PlayerScript>().weaponSlower = (2 / info.weight);
            ammoInput.text = currentAmmo.ToString() + "/" + info.ammo.ToString();
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, rot, Time.deltaTime * 40);
            transform.Translate(new Vector3(0, -Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y")) * Time.deltaTime * 0.4f);
            if (Input.GetButtonDown("Reload") && currentAmmo != info.ammo)
            {
                activeReload = true;
                StartCoroutine(Reload());
            }
            if (Input.GetButtonDown("Fire2") || Input.GetButtonUp("Fire2"))
            {
                sc = true;
            }
            if (Input.GetButton("Fire2") && !activeReload)
            {
                if (sc)
                {
                    if (transform.position == otherPos.position)
                    {
                        sc = false;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, otherPos.position, Time.deltaTime * 2);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, otherPos.position, Time.deltaTime);
                }
            }
            else
            {
                if (sc)
                {
                    if (transform.position == transform.parent.position)
                    {
                        sc = false;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, Time.deltaTime * 2);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, transform.parent.position, Time.deltaTime);
                }
            }
            if (Input.GetButton("Fire1"))
            {
                if (active == false)
                {
                    if (currentAmmo > 0 && !activeReload)
                    {
                        active = true;
                        StartCoroutine(Fire2());
                    }
                    else if (!activeReload)
                    {
                        activeReload = true;
                        StartCoroutine(Reload());
                    }
                }
            }
        }
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForEndOfFrame();
        dontCheck = true;
        rot = transform.localRotation;
        currentAmmo = info.ammo;
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
                            if (child5.GetComponent<WeaponPartInfo>() && child5.GetComponent<WeaponPartInfo>().type == PartType.barrel)
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
        if (active && !activeReload)
        {
            currentAmmo--;
            r = new Vector3(Random.Range(4 / -info.accuracy, 4 / info.accuracy), 0, Random.Range(180, -180));
            GameObject g = Instantiate(info.ammoType.projectile, barrel.position, barrel.rotation);
            Physics.Raycast(transform.parent.parent.position, transform.parent.parent.forward, out hit, Mathf.Infinity);
            if (hit.transform != null && !Input.GetButton("Fire2"))
            {
                g.transform.LookAt(hit.point);
            }
            else
            {
                g.transform.rotation = transform.parent.parent.rotation;
            }
            transform.Translate(-1 / info.stability, 0, 0);
            transform.Rotate(new Vector3(0, 10 / info.stability, 0) * -3);
            g.GetComponent<FriendlyBullet>().damage = info.damage;
            g.GetComponent<FriendlyBullet>().type = gameObject.GetComponent<GripType>().element;
            g.GetComponent<FriendlyBullet>().firerate = info.fireRate;
            g.transform.Rotate(r);
            Destroy(g, 2);
            transform.parent.parent.Rotate(new Vector3(-1 / info.stability, 0, 0) * 10);
            transform.parent.parent.parent.Rotate(new Vector3(0, Random.Range(-1 / info.stability, 1 / info.stability), 0) * 10);
        }
        yield return new WaitForSeconds(1 / info.fireRate);
        if (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            StartCoroutine(Fire2());
        }
        else
        {
            active = false;
        }
    }

    public IEnumerator Reload()
    {
        ammoVisual.fillAmount = 0;
        StartCoroutine(ReloadVisual());
        yield return new WaitForSeconds(5/info.reloadSpeed);
        currentAmmo = info.ammo;
        activeReload = false;
        StopCoroutine(ReloadVisual());
        ammoVisual.fillAmount = 0;
    }

    public IEnumerator ReloadVisual()
    {
        yield return new WaitForSeconds(0.01f);
        ammoVisual.fillAmount += 1 / (5 / info.reloadSpeed) / 50;
        if (activeReload)
        {
            StartCoroutine(ReloadVisual());
        }
        else
        {
            ammoVisual.fillAmount = 0;
        }
    }
}
