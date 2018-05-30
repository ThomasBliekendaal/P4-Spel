using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {
    [Header("Health properties")]
    [Tooltip("Health value.")]
    public float health;
    [Tooltip("Maximum health value.")]
    public float maxHealth;
    [Tooltip("This is the image component which shows the current amount of health.")]
    public Image healthBar;

	void Start () {
        health = maxHealth;
	}
	
	public void Health () {
        healthBar.fillAmount = 1 / maxHealth * health;        
	}

    public void DoDam(float damage)
    {
        health -= damage;
    }
}
