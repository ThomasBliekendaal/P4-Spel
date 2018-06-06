using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallTimer : MonoBehaviour
{
    public Image image;

    public void Start()
    {
        Destroy(gameObject, 12);
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(ReloadVisual());
    }

    public IEnumerator ReloadVisual()
    {
        image.fillAmount -= 0.1f * Time.deltaTime;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(ReloadVisual());
    }
}
