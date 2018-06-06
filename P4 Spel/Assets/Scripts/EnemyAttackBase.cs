using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

public class EnemyAttackBase : MonoBehaviour {

    public enum State {
        BasicMelee , BasicRanged , BasicTank , SuicideBomber , BuffTank , HuntingArty
    };
    public enum State2
    {
        Ranged , Melee , Runner
    };
    [Header("This is the Class info.")]
    [Tooltip("This is the class of the enemy.")]
    public State type;
    [Tooltip("How this enemy attacks")]
    public State2 attackType;
    [Tooltip("Should be size 6")]
    public float[] enemySpeeds;
    [Tooltip("Should be size 6")]
    public float[] enemyHealths;
    [Tooltip("Should be size 6")]
    public float[] enemyDamages;

    [Tooltip("For ranged Classes | should be 2")]
    public float[] ranges;

    public GameObject rangedBullet;

    private bool canAttack;

    // Use this for initialization
    public void Awake()
    {
        EnemyMovement em = gameObject.GetComponent<EnemyMovement>();
        InvokeRepeating("MeleeDamTimer", 0, 0.5f);
        if (type == State.BasicMelee)
        {
            em.health = enemyHealths[0];
            em.speed = enemySpeeds[0];
            em.damage = enemyDamages[0];
        }
        if (type == State.BasicRanged)
        {
            em.health = enemyHealths[1];
            em.speed = enemySpeeds[1];
            em.damage = enemyDamages[1];
        }
        if (type == State.BasicTank)
        {
            em.health = enemyHealths[2];
            em.speed = enemySpeeds[2];
            em.damage = enemyDamages[2];
        }
        if (type == State.SuicideBomber)
        {
            em.health = enemyHealths[3];
            em.speed = enemySpeeds[3];
            em.damage = enemyDamages[3];
        }
        if (type == State.BuffTank)
        {
            em.health = enemyHealths[4];
            em.speed = enemySpeeds[4];
            em.damage = enemyDamages[4];
        }
        if (type == State.HuntingArty)
        {
            em.health = enemyHealths[5];
            em.speed = enemySpeeds[5];
            em.damage = enemyDamages[5];
        }
    }

    // Update is called once per frame
    void Update () {
        EnemyMovement em = gameObject.GetComponent<EnemyMovement>();
        if (em.isAggro == false)
        {
            return;
        }

        if (attackType == State2.Ranged)
        {
            if (type == State.BasicRanged)
            {
                if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) <= ranges[0])
                {
                    transform.LookAt(Camera.main.transform.position);
                    gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
                    GameObject bullet = Instantiate(rangedBullet, gameObject.transform, true);
                    bullet.GetComponent<Bullet>().damage = enemyDamages[2];
                }
            }
            if (type == State.HuntingArty)
            {
                if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) <= ranges[1])
                {
                    transform.LookAt(Camera.main.transform.position);
                    gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, 0);
                    GameObject bullet = Instantiate(rangedBullet, gameObject.transform, true);
                    bullet.GetComponent<Bullet>().damage = enemyDamages[5];
                }
            }
        }
        if (attackType == State2.Runner)
        {
            em.RemoteControl(Camera.main.gameObject.transform);
        }
    }

    public IEnumerator MeleeDamTimer()
    {
        canAttack = true;
        yield return new WaitForSeconds(0.1f);
        canAttack = false;
    }

    public void OnCollisionStay(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject p = col.gameObject;
            if(canAttack == true)
            {
                if (type == State.BasicMelee)
                {
                    p.GetComponent<EnemyMovement>().DoDam(enemyDamages[0]);
                }
                if (type == State.BasicTank)
                {
                    p.GetComponent<EnemyMovement>().DoDam(enemyDamages[2]);
                }
                if (type == State.SuicideBomber)
                {
                    //Start explosion particle
                    p.GetComponent<EnemyMovement>().DoDam(enemyDamages[3]);
                    Destroy(gameObject);
                }
                if (type == State.BuffTank)
                {
                    p.GetComponent<EnemyMovement>().DoDam(enemyDamages[4]);
                }
            }           
        }
    }
}
