using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource _audioSource;
    
    public AudioClip[] _musics;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void PauseMusic()
    {
        _audioSource.Pause();
    }

    public void ChangeMusic(int index)
    {
        _audioSource.clip = _musics[index];
        Debug.Log(_audioSource.clip);
        PlayMusic();
    }



    public void DestroySoundManager()
    {
        Destroy(this.gameObject);
    }

}
