using System.Collections;
using System.Collections.Generic;
using GameCont;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundsCont : MonoBehaviour
{
    public AudioClip[] _sounds;
    private ScoreKeeper _scoreKeeper;
    public AudioSource _audioSource => GetComponent<AudioSource>();

    protected void PlaySound(AudioClip sound, float volume = 1.0f, bool destroyed = false, bool isMusic = false)
    {
        _scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
        if(!isMusic) _audioSource.volume = _scoreKeeper.GetValue("Sound");
        else _audioSource.volume = _scoreKeeper.GetValue("Music");
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position, _audioSource.volume * volume);
        }
        else
        {
            _audioSource.PlayOneShot(sound, volume * _audioSource.volume);
        }
    }
    

}
