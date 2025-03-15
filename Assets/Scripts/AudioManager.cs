using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] paddleSFX, brickSFX, levelSFX; 
    public AudioSource paddleSFXsource, brickSFXsource, levelSFXsource;
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in paddleSFX){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach(Sound s in brickSFX){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        foreach(Sound s in levelSFX){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

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