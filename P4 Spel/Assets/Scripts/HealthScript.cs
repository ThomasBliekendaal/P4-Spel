using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {
    public float health;
    public float maxHealth;
    public Image healthBar;


	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	public void Health () {
        healthBar.fillAmount = 1 / maxHealth * health;
        
	}
}
