using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to your Audio Mixer

    //사운드 옵션에서 효과음 버튼
    public AudioSource sfxTest;
    

    public Slider masterVolumeSlider;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    private void OnEnable()
    {
        float masterVolume, bgmVolume, sfxVolume;

        // Master Volume
        if (audioMixer.GetFloat("MasterVolume", out masterVolume))
        {
            masterVolumeSlider.value = Mathf.Pow(10, masterVolume / 20);
        }

        // BGM Volume
        if (audioMixer.GetFloat("BGMVolume", out bgmVolume))
        {
            bgmVolumeSlider.value = Mathf.Pow(10, bgmVolume / 20);
        }

        // SFX Volume
        if (audioMixer.GetFloat("SFXVolume", out sfxVolume))
        {
            sfxVolumeSlider.value = Mathf.Pow(10, sfxVolume / 20);
        }
    }


    public void PlaySFXSound()
    {
        sfxTest.Play();
    }
    
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}