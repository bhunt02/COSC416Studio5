using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] paddleSFX, brickSFX, levelSFX; 
    public AudioSource paddleSFXsource, brickSFXsource, levelSFXsource;
    

    public void PlaySFX(string name){
        Sound s = Array.Find(paddleSFX, sound => sound.name == name);
        if(s != null){
            paddleSFXsource.clip = s.clip;
            paddleSFXsource.Play();
            s.source.Play();
            return;
        }
        s = Array.Find(brickSFX, sound => sound.name == name);
        if(s != null){
            brickSFXsource.clip = s.clip;
            brickSFXsource.Play();
            s.source.Play();
            return;
        }
        s = Array.Find(levelSFX, sound => sound.name == name);
        if(s != null){
            levelSFXsource.clip = s.clip;
            levelSFXsource.Play();
            s.source.Play();
            return;
        }
        Debug.LogWarning("Sound: " + name + " not found!");
    }
}