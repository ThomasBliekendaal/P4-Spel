using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamagePopUp : MonoBehaviour {
    public float size;
    public float damage;
    public Text text;

    public void Start()
    {
        text.text = damage.ToString();
    }

    public void Update()
    {
        transform.localScale = new Vector3(-size, size, size) * Vector3.Distance(transform.position, GameObject.FindWithTag("MainCamera").transform.position);
        transform.LookAt(GameObject.FindWithTag("MainCamera").transform.position);
    }
}
