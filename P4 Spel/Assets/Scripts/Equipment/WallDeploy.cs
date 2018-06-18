using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeploy : MonoBehaviour
{
    public GameObject wall;
    public GameObject smoke;
    public bool falling;
    public Transform currentWall;
    public float distance;

    public void Start()
    {
        StartCoroutine(Counter());
    }
    public void Update()
    {
        if (falling)
        {
            distance = Vector3.Distance(transform.position, currentWall.position);
            currentWall.Translate(new Vector3(0, -40, 0) * Time.deltaTime);
            if (distance <= 0.5f)
            {
                falling = false;
                GameObject g = Instantiate(smoke, transform.position, transform.rotation);
                Destroy(g, 3);
                wall.transform.position = transform.position;
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator Counter()
    {
        yield return new WaitForSeconds(2);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        GameObject g = Instantiate(wall, transform.position + Vector3.up * 60, transform.rotation);
        currentWall = g.transform;
        falling = true;
    }
}
