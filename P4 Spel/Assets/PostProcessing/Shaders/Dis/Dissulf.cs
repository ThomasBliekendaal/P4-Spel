using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissulf : MonoBehaviour {
    public GameObject top;/// <summary>
    /// hey
    /// </summary>
    public GameObject bottom;
    public bool start;
    //public Text text;
    public Material dissolveMaterial;
    public Material mainMaterial;
    public Renderer dissolvable;
    public float speed, min;/// <summary>
    /// min should be the lowest point of the map
    /// </summary>

    private float currentY, startime;
    public float startingY;
    private float firstStartingY;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Renderer>().material = mainMaterial;
        startingY = top.transform.position.y;
        min = (bottom.transform.position.y - 1);
        firstStartingY = startingY;
        speed = Vector3.Distance(top.transform.position, bottom.transform.position)/1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        //text.text = "Dis Amnt:" + startingY.ToString().Normalize();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            start = true;
        }
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            startingY = firstStartingY;
            start = false;
            gameObject.GetComponent<Renderer>().material = mainMaterial;

        }
        if (start)
        {
            if (startingY > min)
            {
                startingY -= Time.deltaTime * speed;
            }
        }

        dissolveMaterial.SetFloat("_StartingY", startingY);

        if (startingY <= min)
        {
            Die();
            start = false;
        }
    }

    public void Die()
    {
        p.Stop();
        Destroy(gameObject, 1);
    }

    public void GotHit()
    {
        startingY = firstStartingY;
        gameObject.GetComponent<Renderer>().material = dissolveMaterial;
        start = true;
    }

    public void Disintegrate()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(0, 7), Random.Range(0, 7), Random.Range(0, 7));
        gameObject.GetComponent<Rigidbody>().AddTorque(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
        p.Play();
        speed /= 2.5f;
        GotHit();
    }
}
