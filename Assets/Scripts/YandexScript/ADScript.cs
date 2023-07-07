using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ADScript : MonoBehaviour
{
    private const float CheckTimer = 150f;
    private const float TimeOffset = 5f;

    //public string nameScene;
    private string lastIsAdsOpen = null;
   // private float timer;

   // public TextMeshProUGUI adWarningScene;
   // public TextMeshProUGUI adWarningSetting;
   // public TextMeshProUGUI adWarningCategory;

    public void ShareFriend(){
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLPluginJS.ShareFunction();
#endif
    }

    public void ShowAdInterstitial(){

#if UNITY_WEBGL && !UNITY_EDITOR
    	            WebGLPluginJS.InterstitialFunction();
#endif

      
    }
/*
    IEnumerator Courutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        adWarningScene.gameObject.SetActive(false);
        //adWarningSetting.gameObject.SetActive(false);
        //adWarningCategory.gameObject.SetActive(false);
    }
*/
    public void ShowAdReward(){
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif
       // sliderHome.value += rewardBonusSliderHome;
    	//if(sliderFuelCar.value<=lowBalanceFuel) sliderFuelCar.value += rewardBonusSliderFuel;
    }

    private void Start()
    {
        ShowAdInterstitial();
    }

    private void Update()
    {
        //var deltaTime = Time.deltaTime;
       // timer += deltaTime;

        CheckAds();
        /*
        if (timer + TimeOffset > CheckTimer)
        {
            adWarningScene.gameObject.SetActive(true);
            //adWarningSetting.gameObject.SetActive(true);
            //adWarningCategory.gameObject.SetActive(true);
            if (timer > CheckTimer)
            {
                ShowAdInterstitial();
            }
            else
            {
                var timeRest = Math.Floor(CheckTimer - timer) + 1;
                adWarningScene.text = $"Реклама через: {timeRest}";
               // adWarningSetting.text = $"Реклама через: {timeRest}";
                //adWarningCategory.text = $"Реклама через: {timeRest}";
            }
        } 
        */
    }
    /* //В этом проекте работают в PROSoundManager
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
    */               
    public void AdsClosed()
    {
        //timer = 0f;
        Time.timeScale = 1;
    }

    public void ForReward()
    {
        Time.timeScale = 0;
    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        var adsOpen = WebGLPluginJS.GetAdsOpen();
        if (lastIsAdsOpen == null) {
            lastIsAdsOpen = adsOpen;
        }

        if (adsOpen == "yes")
        {
            PlayerPrefs.SetInt("AdsOpen", 1);
           // AudioListener.pause = true;
            lastIsAdsOpen = "yes";
        }
        else
        {
            //Коничлась реклама
            PlayerPrefs.SetInt("AdsOpen", 0);
           // AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
                AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }
}
