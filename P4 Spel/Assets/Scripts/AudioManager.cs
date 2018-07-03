using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public List<AudioClip> sfx = new List<AudioClip>();
    public AudioSource soundplayer;
    public float volume;
    public float turretVolume;

    public void PlaySound(int sound)
    {
        soundplayer.PlayOneShot(sfx[sound], volume);
    }
    public void PlayTurretSound(int sound, AudioSource source)
    {
        source.PlayOneShot(sfx[sound], turretVolume);
    }
}
