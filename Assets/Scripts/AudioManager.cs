using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] SFXsounds;
    public AudioSource SFXsoundsource;
    private void Awake(){
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in SFXsounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
        
    }

    public void PlaySFX(string name){
        Sound s = Array.Find(SFXsounds, sound => sound.name == name);
        if(s != null){
            SFXsoundsource.clip = s.clip;
            SFXsoundsource.Play();
            s.source.Play();
            return;
        }
        
        Debug.LogWarning("Sound: " + name + " not found!");
    }
}