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

    public Text MasterLabel;
    public Text MusicLabel;
    public Text SFXLabel;
    public Text ResolutionLabel;
    // public AudioSource sfxSound;

    public Resolutions[] resolutions;


    //----

    [System.Serializable]
    public struct Resolutions
    {
        public int Horizontal, Vertical;
    }
    //------
    private void Start()
    {
        FullScreen.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
            Vsync.isOn = false;
        else
            Vsync.isOn = true;

        MasterLabel.text = (MasterVolume.value + 80).ToString();
        MusicLabel.text = (MusicVolume.value + 80).ToString();
        SFXLabel.text = (SFXVolume.value + 80).ToString();
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

        //set resolution && fullscreen

        Screen.SetResolution(resolutions[selectedResolution].Horizontal, resolutions[selectedResolution].Vertical, FullScreen.isOn);

    }
    private void ResolutionUpdateText()
    {
        ResolutionLabel.text = resolutions[selectedResolution].Horizontal + " X " + resolutions[selectedResolution].Vertical;
    }

    public void IncreaseResolution()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Length - 1)
            selectedResolution = resolutions.Length - 1;
        ResolutionUpdateText();
    }
    public void DecresResolution()
    {
        selectedResolution--;
        if (selectedResolution < 0)
            selectedResolution = 0;
        ResolutionUpdateText();
    }


    public void SetMasterVolume()
    {
        MasterLabel.text = (MasterVolume.value + 80).ToString();
        TheMixer.SetFloat("MasterVol", MasterVolume.value);
    }
    public void SetMusicVolume()
    {
        MusicLabel.text = (MusicVolume.value + 80).ToString();
        TheMixer.SetFloat("MusicVol", MusicVolume.value);
    }
    public void SetSFXVolume()
    {
        SFXLabel.text = (SFXVolume.value + 80).ToString();
        TheMixer.SetFloat("SFXVol", SFXVolume.value);
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
