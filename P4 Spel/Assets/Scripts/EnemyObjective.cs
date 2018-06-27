using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyObjective : MonoBehaviour {

    [Header("Health properties")]
    [Tooltip("Health value.")]
    public float health;
    [Tooltip("Maximum health value.")]
    public float maxHealth;
    [Tooltip("Minimum health value.")]
    public float minHealth;
    [Tooltip("This is the image component which shows the current amount of health.")]
    public Image healthBar;

    public GameObject curtain;

    private bool canTake = true;

    // Use this for initialization
    void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if(health <= minHealth)
        {
            EnemiesWin();
        }
        Health();
	}

    public void Health()
    {
        if (healthBar)
        {
            healthBar.fillAmount = 1 / maxHealth * health;
        }
    }

    public void EnemiesWin()
    {
        Destroy(gameObject,0.2f); //this destroys the curtain (this script should be on the curtain! // this needs to be on the lowest line)
    }

    public void DoDam(float damage)
    {
        health -= damage;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            DoDam(collision.gameObject.GetComponent<EnemyMovement>().damage);
        }
    }
    public void OnCollisionStay(Collision collision)
    {
        if (canTake)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                DoDam(collision.gameObject.GetComponent<EnemyMovement>().damage);
            }
            canTake = false;
            StartCoroutine(TakeTimer());
        }      
    }

    public IEnumerator TakeTimer()
    {
        yield return new WaitForSeconds(0.7f);
        canTake = true;
    }
}
