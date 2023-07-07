using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public Sound[] Sounds;
   // public Sound[] Effects;
    private AudioSource[] soundAudioSources;

    public string mainTheme;
    private bool isAds;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Sound")) PlayerPrefs.SetInt("Sound", 1);
       // if (!PlayerPrefs.HasKey("Effect")) PlayerPrefs.SetInt("Effect", 1);
       // if (!PlayerPrefs.HasKey("SoundSlider")) PlayerPrefs.SetFloat("SoundSlider", 1f);
      //  if (!PlayerPrefs.HasKey("EffectSlider")) PlayerPrefs.SetFloat("EffectSlider", 1f);
        //var i = 0;

        
        foreach (Sound sound in Sounds)
        {
            // sound.AudioSource = gameObject.AddComponent<AudioSource>();
            // sound.AudioSource.clip = sound.clip;

            sound.AudioSource.volume = sound.Volume;
            sound.AudioSource.mute = sound.Mute;
            // soundAudioSources[i] = sound.AudioSource;
            // i++;
        }
        /*
        foreach (Sound effect in Effects)
        {
            // sound.AudioSource = gameObject.AddComponent<AudioSource>();
            //sound.AudioSource.clip = sound.clip;
            effect.AudioSource.volume = effect.Volume;
            effect.AudioSource.mute = effect.Mute;
        }
        */
    }

    public void SearchSound()
    {
        
    }

    private void Start()
    {
        // Play(mainTheme);
    }

    private void Update()
    {
       // CheckAds();
        SoundCheck();
       // EffectCheck();
      //  SoundSliderCheck();
       // EffectSliderCheck();
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.nameSound == soundName);

        if (s == null)
        {
            Debug.LogError(soundName + " клип не найден!");
            return;
        }
        s.AudioSource.Play();
    }

    public void ChangeSound(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

    public void SoundCheck()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            foreach (Sound sound in Sounds)
            {
                sound.AudioSource.mute = false;
            }
        }
        else
        {
            foreach (Sound sound in Sounds)
            {
                sound.AudioSource.mute = true;
            }
        }
    }
    /*
    public void EffectCheck()
    {
        if (PlayerPrefs.GetInt("Effect") == 1)
        {
            foreach (Sound effect in Effects)
            {
                effect.AudioSource.mute = false;
            }
        }
        else
        {
            foreach (Sound effect in Effects)
            {
                effect.AudioSource.mute = true;
            }
        }
    }
    
    public void SoundSliderCheck()
    {
        foreach (Sound sound in Sounds)
        {
            sound.AudioSource.volume = PlayerPrefs.GetFloat("SoundSlider");
        }
    }

    public void EffectSliderCheck()
    {
            foreach (Sound effect in Effects)
            {
                effect.AudioSource.volume = PlayerPrefs.GetFloat("EffectSlider");
            }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AudioListener.pause = false;
            
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(WebGLPluginJS.GetAdsOpen() == "yes")
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
#endif
    }
    */
}
