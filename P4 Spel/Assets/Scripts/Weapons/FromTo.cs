using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromTo : MonoBehaviour {

    public GameObject from;
    public GameObject to;

    public void OnButtonPress()
    {
        from.SetActive(false);
        if (to)
        {
            to.SetActive(true);
        }
    }
}
