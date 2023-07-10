using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions.Must;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Dictionary<string, AudioPrefab> SoundSources = new Dictionary<string, AudioPrefab>();
    public Dictionary<string, AudioPrefab> MusicSources = new Dictionary<string, AudioPrefab>();

    private List<AudioPrefab> pausedSources = new List<AudioPrefab> ();

    public float SoundVolume=1, MusicVolume=1;
    public bool SceneIsloading;
    public bool mute;
    void Awake()
    {
        mute = false;
        if(Instance == null)
        Instance = this;

      /*  MusicVolumeSlider.maxValue = 1;
        MusicVolumeSlider.minValue = 0;
        SoundVolumeSlider.maxValue = 1;
        SoundVolumeSlider.minValue = 0;*/
        DontDestroyOnLoad(gameObject);
        SceneIsloading = false;
    }



    public void PlaySound(string name)
    {
        if(SceneIsloading)
            return;

        if (SoundSources.ContainsKey(name))
        {
            if (!SoundSources[name].AudioSource.isPlaying)
            {
                SoundSources[name].AudioSource.Play();
            }
        }
        else if (MusicSources.ContainsKey(name))
        {
            if (!MusicSources[name].AudioSource.isPlaying)
                MusicSources[name].AudioSource.Play();
        }
        else
        {
            Debug.LogError("Sound Not Found " + name);
        }
    }
    public void StopSound(string name,float delay)
    {
        if(SceneIsloading)
            return ;

        if (SoundSources.ContainsKey(name))
        {
            if (SoundSources[name].AudioSource.isPlaying)
                StartCoroutine(IStopSound(SoundSources[name], delay));
        }
        else if (MusicSources.ContainsKey(name))
        {
            if (MusicSources[name].AudioSource.isPlaying)
                StartCoroutine(IStopSound(MusicSources[name], delay));
        }
        else
        {
            Debug.LogError("Sound Not Found " + name);
        }
    }
    private IEnumerator IStopSound(AudioPrefab audio,float delay)
    {
  
        float t = 0; 
        while (t < delay)
        {
            audio.AudioSource.volume = Mathf.Lerp(audio.volume, 0, t/delay);
            t += Time.deltaTime;
            yield return null;
        }
        audio.AudioSource.Stop();
        
    }


    private void Update()
    {
        if (mute)
        {
            foreach (AudioPrefab soundSource in SoundSources.Values)
            {
                soundSource.AudioSource.volume = soundSource.volume * 0;
            }
            foreach (AudioPrefab musicSource in MusicSources.Values)
            {
                musicSource.AudioSource.volume = musicSource.volume * 0;
            }
        }else
        {
            UpdateVolumeValues();
        }


    }

    private void UpdateVolumeValues()
    {
        foreach (AudioPrefab soundSource in SoundSources.Values)
        {
            soundSource.AudioSource.volume = soundSource.volume * SoundVolume;

        }
        foreach (AudioPrefab musicSource in MusicSources.Values)
        {
            musicSource.AudioSource.volume = musicSource.volume * MusicVolume;
        }
    }

    public void MusicVolumeChanged(float value)
    {
        MusicVolume = value;
        UpdateVolumeValues();
        mute = false;
        if(SceneManager.GetActiveScene().name == "MainMenuee")
        MenuController.instance._muteButton.style.backgroundImage = MenuController.instance.unmuteSprite.texture;
    }
    public void SoundVolumeChanged(float value)
    {
        SoundVolume = value;
        UpdateVolumeValues();
        mute = false;
        if (SceneManager.GetActiveScene().name == "MainMenuee")
            MenuController.instance._muteButton.style.backgroundImage = MenuController.instance.unmuteSprite.texture;

    }
    public void pauseall()
    {
        pausedSources.Clear();
        foreach (AudioPrefab soundSource in SoundSources.Values)
        {
            if (soundSource.AudioSource.isPlaying)
            {
            soundSource.AudioSource.Pause();
            pausedSources.Add(soundSource);

            }
        }
        foreach (AudioPrefab musicSource in MusicSources.Values)
        {
            if (musicSource.AudioSource.isPlaying)
            {
                musicSource.AudioSource.Pause();
                pausedSources.Add(musicSource);
            }
        }
    }
    public void Resumeall()
    {

        foreach (AudioPrefab audioSource in pausedSources)
        {
            audioSource.AudioSource.Play();
            
        }
      
    }



}
