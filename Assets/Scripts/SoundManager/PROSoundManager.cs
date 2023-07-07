using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class PROSoundManager : MonoBehaviour
{
    private void Awake()
    {
            if (!PlayerPrefs.HasKey("Sound")) PlayerPrefs.SetInt("Sound", 1);
            if (!PlayerPrefs.HasKey("AdsOpen")) PlayerPrefs.SetInt("AdsOpen", 0);
        // if (!PlayerPrefs.HasKey("SoundSlider")) PlayerPrefs.SetFloat("SoundSlider", 1f);
    }
    private void Update()
    {
        SoundCheck();
        
       //SoundSliderCheck();
    }
    public void SoundCheck()
    {
        if (PlayerPrefs.GetInt("Sound") == 1 && PlayerPrefs.GetInt("AdsOpen") == 0)
        {
            AudioListener.pause = false;
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.pause = true;
            AudioListener.volume = 0;
        }
    }
    public void SoundSliderCheck()
    {
       // AudioListener.volume = PlayerPrefs.GetFloat("SoundSlider");
        Debug.Log(AudioListener.volume);   
    }

    public void SoundOnOff(int isSoundOn)
    {
        PlayerPrefs.SetInt("Sound", isSoundOn);
    }

    private void OnApplicationFocus(bool focus)
    {
        SoundCheck();
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
        SoundCheck();
        if (!pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }
}
