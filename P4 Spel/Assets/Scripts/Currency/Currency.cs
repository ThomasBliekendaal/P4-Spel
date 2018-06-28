using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour {

    public int currency;
    public Text t;

    public void Update()
    {
        if (t)
        {
            t.text = "$" + currency.ToString();
        }       
    }
}
