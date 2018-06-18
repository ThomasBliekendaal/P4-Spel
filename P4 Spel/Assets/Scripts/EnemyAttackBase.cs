using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class EnemyAttackBase : MonoBehaviour
{
    public enum State
    {
        BasicMelee, BasicRanged, BasicTank, SuicideBomber, BuffTank, HuntingArty
    };
    public enum State2
    {
        Ranged, Melee, Runner
    };
    [Header("This is the Class info.")]
    [Tooltip("This is the class of the enemy.")]
    public State type;
    [Tooltip("How this enemy attacks")]
    public State2 attackType;
    [Tooltip("The speed of the enemy")]
    public EnemyClass enemySpeeds;
    [Tooltip("The amount of health this enemy has")]
    public EnemyClass enemyHealths;
    [Tooltip("The amount of damage the enemy does")]
    public EnemyClass enemyDamages;

    [Tooltip("The explosion particle system for the bomber.")]
    public ParticleSystem pS;

    [Tooltip("The range of the ranged classes")]
    public EnemyRanges ranges;
    [Tooltip("Firing Rate per second")]
    public float shootingSpeed;

    public GameObject rangedBullet;

    private bool canAttack;
    private bool canShoot = true;

    // This is the mortar script made by jelmer and edited by stefan
    public GameObject shellStart;
    public GameObject shellImpact;
    public bool active;

    // Use this for initialization
    public void Awake()
    {
        EnemyMovement em = gameObject.GetComponent<EnemyMovement>();
        Renderer r = gameObject.GetComponent<Renderer>();
        if (type == State.BasicMelee)
        {
            em.health = enemyHealths.BasicMelee;
            em.speed = enemySpeeds.BasicMelee;
            em.damage = enemyDamages.BasicMelee;
            r.material.color = Color.blue;
        }
        if (type == State.BasicRanged)
        {
            em.health = enemyHealths.BasicRanged;
            em.speed = enemySpeeds.BasicRanged;
            em.damage = enemyDamages.BasicRanged;
            r.material.color = Color.red;
        }
        if (type == State.BasicTank)
        {
            em.health = enemyHealths.BasicTank;
            em.speed = enemySpeeds.BasicTank;
            em.damage = enemyDamages.BasicTank;
            r.material.color = Color.green;
        }
        if (type == State.SuicideBomber)
        {
            em.health = enemyHealths.SuicideBomber;
            em.speed = enemySpeeds.SuicideBomber;
            em.damage = enemyDamages.SuicideBomber;
            r.material.color = Color.white;
        }
        if (type == State.BuffTank)
        {
            em.health = enemyHealths.BuffTank;
            em.speed = enemySpeeds.BuffTank;
            em.damage = enemyDamages.BuffTank;
            r.material.color = Color.magenta;
        }
        if (type == State.HuntingArty)
        {
            em.health = enemyHealths.HuntingArty;
            em.speed = enemySpeeds.HuntingArty;
            em.damage = enemyDamages.HuntingArty;
            r.material.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement em = gameObject.GetComponent<EnemyMovement>();
        if (em.isAggro == false)
        {
            return;
        }

        if(type == State.BuffTank)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length ; i++)
            {
                enemies[i].GetComponent<EnemyMovement>().health += enemies[i].GetComponent<EnemyMovement>().maxHealth / 200 * Time.deltaTime;
            }
        }

        if(type == State.SuicideBomber)
        {
            if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) <= 1.5f)
            {
                Destroy(gameObject, 3);
                Instantiate(pS, transform.position, transform.rotation);
                Collider[] colliders = Physics.OverlapSphere(transform.position, 10);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject.tag == "Enemy")
                    {
                        colliders[i].GetComponent<EnemyMovement>().DoDam(enemyDamages.SuicideBomber * (1 / Vector3.Distance(transform.position, colliders[i].transform.position)));
                    }
                    else
                    {
                        if (colliders[i].gameObject.tag == "Player")
                        {
                            colliders[i].GetComponent<HealthScript>().DoDam(enemyDamages.SuicideBomber * (1 / Vector3.Distance(transform.position, colliders[i].transform.position)));
                        }
                    }
                }
            }
        }

        if (attackType == State2.Ranged)
        {
            if (type == State.BasicRanged)
            {
                em.aggroRadius = ranges.BasicRangedRange;
                if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < ranges.BasicRangedRange)
                {
                    transform.LookAt(Camera.main.transform.position);
                    gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
                    if (canShoot == true)
                    {
                        GameObject bullet = Instantiate(rangedBullet, gameObject.transform.position, transform.rotation);
                        bullet.GetComponent<Bullet>().damage = enemyDamages.BasicRanged;
                        canShoot = false;
                        StartCoroutine(CanShooter(shootingSpeed));
                    }

                }
            }
            if (type == State.HuntingArty)
            {
                em.aggroRadius = ranges.HuntingArtyRange;

                if (!active)
                {
                    StartCoroutine(ShootTimer());
                    active = true;
                }
            }
        }
    }

    public IEnumerator MeleeDamTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        canAttack = true;
    }

    public IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(shootingSpeed);
        StartCoroutine(Fire());
    }

    public IEnumerator Fire()
    {
        GameObject g = Instantiate(shellStart, transform.position, transform.rotation);
        yield return new WaitForSeconds(1);
        Destroy(g);
        Impact(Camera.main.transform.position);
    }

    public void Impact(Vector3 location)
    {
        Instantiate(shellImpact, (new Vector3(location.x,location.y + 70,location.z)),(gameObject.transform.rotation));
        active = false;
    }

    public IEnumerator CanShooter(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject p = col.gameObject;
            if (canAttack == true)
            {
                canAttack = false;
                MeleeDamTimer();
                if (type == State.BasicMelee)
                {
                    p.GetComponent<PlayerScript>().DoDam(enemyDamages.BasicMelee);
                }
                if (type == State.BasicTank)
                {
                    p.GetComponent<PlayerScript>().DoDam(enemyDamages.BasicTank);
                }
                if (type == State.BuffTank)
                {
                    p.GetComponent<PlayerScript>().DoDam(enemyDamages.BuffTank);
                }
            }
        }
    }
}

[System.Serializable]
public class EnemyClass
{
    public float BasicMelee;
    public float BasicRanged;
    public float BasicTank;
    public float SuicideBomber;
    public float BuffTank;
    public float HuntingArty;
}

[System.Serializable]
public class EnemyRanges
{
    public float BasicRangedRange;
    public float HuntingArtyRange;
}