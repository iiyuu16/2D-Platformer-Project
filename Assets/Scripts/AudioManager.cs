using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum VolumeType
    {
        Master,
        SFX,
        Ambiance,
        BG
    }

    [System.Serializable]
    public class Audio
    {
        public string audioName;
        public VolumeType volumeType;
        public AudioClip clip;
        public float volume = 1;
        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    public AudioMixerGroup masterMixer;
    public AudioMixerGroup sfxMixer;
    public AudioMixerGroup ambianceMixer;
    public AudioMixerGroup bgMixer;

    public AudioMixer audioMixer;

    public Audio[] audios;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

        for (int i = 0; i < audios.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            Audio getAudio = audios[i];

            audioSource.volume = getAudio.volume;

            audioSource.outputAudioMixerGroup = GetMixer(getAudio.volumeType);

            audioSource.loop = getAudio.loop;
            audioSource.clip = getAudio.clip;

            getAudio.source = audioSource;
        }
    }

    public Audio GetAudio(string audioName)
    {
        Audio audio = Array.Find(audios, findAudio => findAudio.audioName == audioName);
        return audio;
    }

    public void PlayAudio(string audioName)
    {
        Audio audio = GetAudio(audioName);

        if (audio == null) return;

        audio.source.Play();
    }

    public void StopAudio(string audioName)
    {
        Audio audio = GetAudio(audioName);
        
        if (audio == null) return;

        audio.source.Stop();
    }

    public void StopAudio()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            Audio getAudio = audios[i];

            getAudio.source.Stop();
        }
    }

    private AudioMixerGroup GetMixer(VolumeType volumeType)
    {
        AudioMixerGroup[] audioMixerGroup = audioMixer.FindMatchingGroups("Master");
        AudioMixerGroup getMixer;

        switch (volumeType)
        {
            case VolumeType.Ambiance:
                getMixer = audioMixerGroup[1];
                break;
            case VolumeType.SFX:
                getMixer = audioMixerGroup[2];
                break;
            case VolumeType.BG:
                getMixer = audioMixerGroup[3];
                break;
            default:
                getMixer = audioMixerGroup[0];
                break;
        }

        return getMixer;
    }

    public void SetVolume(string name, float volume)
    {
        audioMixer.SetFloat(name, volume);
    }
}
