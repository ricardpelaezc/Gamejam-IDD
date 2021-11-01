using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    private int selectedResolution = 0;

    public Toggle FullScreen;
    public Toggle Vsync;

    public AudioMixer TheMixer;

    public Slider MasterVolume;
    public Slider MusicVolume;
    public Slider SFXVolume;

    // public AudioSource sfxSound;


    //----
    private CanvasGroup cg;
    public void Show()
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void Hide()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
    //------
    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        FullScreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
            Vsync.isOn = false;
        else
            Vsync.isOn = true;
    }

    public void ApplyGraphics()
    {
        //apply fullsceen
      //  Screen.fullScreen = FullScreen.isOn;
        //apply vsync
        if (Vsync.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;


        Screen.fullScreen = FullScreen.isOn;
    }


    public void SetMasterVolume()
    {
        TheMixer.SetFloat("MasterVol", Mathf.Log10(MasterVolume.value) * 20);

        if (MasterVolume.value<= 0)
        {
            TheMixer.SetFloat("MasterVol", -80);
        }
    }
    public void SetMusicVolume()
    {
        TheMixer.SetFloat("MusicVol", Mathf.Log10(MusicVolume.value) * 20);

        if (MusicVolume.value <= 0)
        {
            TheMixer.SetFloat("MusicVol", -80);
        }
    }
    public void SetSFXVolume()
    {
        TheMixer.SetFloat("SFXVol", Mathf.Log10(SFXVolume.value) * 20);

        if (SFXVolume.value <= 0)
        {
            TheMixer.SetFloat("SFXVol", -80);
        }
    }

    //public void PlaySFXLoop()
    //{
    //    sfxSound.Play();
    //}

    //public void StopSFXLoop()
    //{
    //    sfxSound.Stop();
    //}
}
