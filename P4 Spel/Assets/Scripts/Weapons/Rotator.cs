using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public Vector3 r;

    public void Update()
    {
        transform.Rotate(r * Time.deltaTime);
    }
}
