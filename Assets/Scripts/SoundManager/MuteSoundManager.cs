using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSoundManager : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)
        {
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        
    }

}
