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
    [Tooltip("Minimum health value.")]
    public float minHealth;
    [Tooltip("This is the image component which shows the current amount of health.")]
    public Image healthBar;

    private void Awake()
    {
        health = maxHealth;
    }

    void Update()
    {

    }

    public void Health () {
        healthBar.fillAmount = 1 / maxHealth * health;        
	}

    public void DoDam(float damage)
    {
        health -= damage;
    }
}
