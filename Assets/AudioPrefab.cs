using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefab : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AudioSource;
    public bool autoname;
    public string audioname;

    public  float volume;
    public AudioTypes type;
    public enum AudioTypes
    {
        sound,music
    }
  
    void Start()
    {
        if (autoname)
            audioname = gameObject.transform.parent.name;

        AudioSource = GetComponent<AudioSource>();

        volume =AudioSource. volume;
        if (type == AudioTypes.sound)
        {
            if(!AudioManager.Instance.SoundSources.ContainsKey(audioname))
            AudioManager.Instance.SoundSources.Add(audioname, this);
          
        }
        else if (type == AudioTypes.music)
        {
            if (!AudioManager.Instance.SoundSources.ContainsKey(audioname))
                AudioManager.Instance.MusicSources.Add(audioname,this);

        }
       AudioManager.Instance.SceneIsloading = false;
    }


    public void stopsound(float delay)
    {
        AudioManager.Instance.StopSound(audioname,delay);
    }
    public void play()
    {

        AudioSource.Play();
    }
    private void OnDisable()
    {
        if(type == AudioTypes.sound)
        AudioManager.Instance.SoundSources.Remove(audioname);
        else
        AudioManager.Instance.MusicSources.Remove(audioname);
    }
}


