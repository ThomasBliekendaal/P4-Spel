using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDissolve : MonoBehaviour {

    public bool start;
    public Material dissolveMaterial;
    public Material mainMaterial;
    public float speed, max;
    public ParticleSystem p;
    public ParticleSystem p2;
    public bool ended = false;
    public AudioSource a;
    public AudioSource a2;
    public GameObject lightning;
    public GameObject lightning2;
    public GameObject lightning3;
    public GameObject lightHolder;
    public AudioSource a3;

    private float currentY, startime;

    private void Start()
    {
        gameObject.GetComponent<Renderer>().material = dissolveMaterial;
    }

    private void Update()
    {
        if (start)
        {
            if (currentY < max)
            {
                currentY += Time.deltaTime * speed;
            }
        }

        dissolveMaterial.SetFloat("_DissolveY", currentY);

        if (currentY >= max)
        {
            gameObject.GetComponent<Renderer>().material = mainMaterial;
            p.Stop(false);
            EndParticles();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TriggerEffect();
            if (!start)
            {
                start = true;
                p.Play(false);
            }
        }
    }

    private void TriggerEffect()
    {
        startime = Time.time;
        currentY = 0;
        gameObject.GetComponent<Renderer>().material = dissolveMaterial;
        p.Play(false);
        ended = false;
        a2.Play();
        a3.Stop();
        lightHolder.GetComponent<Light>().enabled = false;
    }

    private void EndParticles()
    {
        if (!ended)
        {
            p2.Play(false);
            ended = true;
            a.Play();
            a3.Play();
            lightHolder.GetComponent<Light>().enabled = true;
        }
    }
}
