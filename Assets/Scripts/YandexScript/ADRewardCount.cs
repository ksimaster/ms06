using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADRewardCount : MonoBehaviour
{
    private string lastIsAdsOpen = null;


    void Start()
    {
        
    }

    void Update()
    {
        //if (compCanvasResult.enabled) isCanvasResult = 1;
        //isImageReticle = compImageReticle.enabled ? 1 : 0;
        /*
        if (compImageReticle.enabled || compCanvasResult.enabled) 
        {
            rewardPanelText.SetActive(false);
            //Time.timeScale = 1;
        }
        else
        {
            rewardPanelText.SetActive(true);
            //Time.timeScale = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && !compImageReticle.enabled && !compCanvasResult.enabled && !rewardPanel.activeSelf)
        {
            
            if (!compCanvasResult.enabled)
            {
                ForReward();

                ShowAdRewardForLife();
                rewardPanel.SetActive(true);
                messageText.SetActive(false);
                Debug.Log("MainBody_HP увеличено на 50 %");
            }
           
            
            
        }
        */
    }

    public void ShowAdRewardForLife()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif
        
    }
    public void ForReward()
    {
        Time.timeScale = 0;
       // rewardPanelText.SetActive(false);
        //rewardPanelText.SetActive(true);
    }
    public void AdsClosed()
    {
        // timer = 0f;
        Time.timeScale = 1;
       // rewardPanelText.SetActive(true);
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
            AudioListener.pause = true;
            lastIsAdsOpen = "yes";
        }
        else
        {
            //Коничлась реклама
            PlayerPrefs.SetInt("AdsOpen", 0);
           // if (PlayerPrefs.GetInt("Sound") == 1) AudioListener.pause = false;
            if (lastIsAdsOpen == "yes") {
               // AdsClosed();
                lastIsAdsOpen = "no";
            }
        }
#endif
    }


}
