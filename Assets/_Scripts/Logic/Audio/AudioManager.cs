using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public Sound[] SFXsounds;
    public AudioSource SFXsource;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        // Ensure exactly one AudioListener exists in the scene
        AudioListener[] listeners = UnityEngine.Object.FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        foreach (AudioListener listener in listeners) {
            if(listener.gameObject != gameObject){
                Destroy(listener);
            }
        }
        // Ensure each Sound has its own AudioSource
        foreach(Sound s in SFXsounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = 0; // Force 2D audio for testing
        }
    }

    public void PlaySFX(string name){
        Debug.Log($"Attempting to play sound: {name}"); // Debug log
        Sound s = Array.Find(SFXsounds, sound => sound.name == name);
        if(s != null){
            Debug.Log($"Playing sound: {name}"); // Debug logoSource settings - Volume: {s.source.volume}, SpatialBlend: {s.source.spatialBlend}, Position: {s.source.transform.position}");
            s.source.Play();
            Debug.Log(s.source);
   
            return;   
        }
        Debug.LogWarning($"Sound: {name} not found!");   
    }   }

