using System;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager:MonoBehaviour
{
    public Audio[] sound;
    public static Audiomanager Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    foreach (Audio a in sound)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop= a.loop;
        }
    }
    public void Play(string name)
    {
        Audio s = Array.Find(sound, sound => sound.name == name);
        s.source.Play();
    }
}
