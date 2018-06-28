using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeh : MonoBehaviour {

    public string timeh;
    public int minutes;
    public int seconds;
    public Text t;

	// Use this for initialization
	void Start () {
        InvokeRepeating("AddSecond" ,0f ,1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
        timeh = minutes + ":" + seconds;
        t.text = timeh;
	}

    public void AddSecond()
    {
        seconds++;
    }
}
