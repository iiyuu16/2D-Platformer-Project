using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsHUD : MonoBehaviour
{
    public GameObject settingHud;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider ambianceSlider;
    public Slider bgSlider;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.instance;

        audioManager.audioMixer.GetFloat("Master", out float masterValue);
        masterSlider.value = masterValue;

        audioManager.audioMixer.GetFloat("SFX", out float sfxValue);
        sfxSlider.value = sfxValue;

        audioManager.audioMixer.GetFloat("Ambiance", out float ambianceValue);
        ambianceSlider.value = ambianceValue;

        audioManager.audioMixer.GetFloat("BG", out float bgValue);
        bgSlider.value = bgValue;
    }

    private void Update()
    {
        if (audioManager == null) return;

        audioManager.SetVolume("Master", masterSlider.value);
        audioManager.SetVolume("SFX", sfxSlider.value);
        audioManager.SetVolume("Ambiance", ambianceSlider.value);
        audioManager.SetVolume("BG", bgSlider.value);
    }

    public void SetSettingHUD(bool active)
    {
        settingHud.SetActive(active);
    }
}
