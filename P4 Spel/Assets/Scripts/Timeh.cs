using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeh : MonoBehaviour {

    public string timeh;
    public int minutes;
    public int seconds;

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
        print(timeh);
	}

    public void AddSecond()
    {
        seconds++;
    }
}
