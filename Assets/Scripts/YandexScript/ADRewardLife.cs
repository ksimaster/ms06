using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ADRewardLife : MonoBehaviour
{
    // Damage_Control_Center_CS
    private float maxLife;
    private float maxHeadLife;
    private float addLife;
    private float addHeadLife;
    private string lastIsAdsOpen = null;
    private GameObject canvasResult;
    private Canvas compCanvasResult;
    private GameObject reticle;
    private Image compImageReticle;
    private GameObject rewardPanelText;
    private GameObject canvasPause;
    private Canvas compCanvasPause;
    private GameObject rewardPanel;
    private GameObject messageText;

    private int isCanvasResult = 0;
    private int isImageReticle = 0;

    void Start()
    {
        canvasResult = GameObject.Find("Canvas_Result");
        compCanvasResult = canvasResult.GetComponent<Canvas>();
        reticle = GameObject.Find("Reticle");
        compImageReticle = reticle.GetComponent<Image>();
        rewardPanelText = GameObject.Find("RewardPanelTextSpace");
        rewardPanel = GameObject.Find("PanelForReward");
        rewardPanel.SetActive(false);
        messageText = GameObject.Find("Text_Message");
       // maxHeadLife = gameObject.GetComponent<Damage_Control_Center_CS>().Turret_Props[0].hitPoints;
        //maxLife = gameObject.GetComponent<Damage_Control_Center_CS>().MainBody_HP;
        addLife = 0.5f * maxLife;
        addHeadLife = 0.5f * maxHeadLife;

        // gameObject.GetComponent<Damage_Control_Center_CS>().MainBody_Damaged(-50);
        //Debug.Log("MainBody_HP увеличено на 50");
    }

    void Update()
    {
        //if (compCanvasResult.enabled) isCanvasResult = 1;
        //isImageReticle = compImageReticle.enabled ? 1 : 0;
        
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
        rewardPanelText.SetActive(false);
        //rewardPanelText.SetActive(true);
    }
    public void AdsClosed()
    {
        // timer = 0f;
        Time.timeScale = 1;
        rewardPanelText.SetActive(true);
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
