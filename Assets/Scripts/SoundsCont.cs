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
        if(!isMusic) _audioSource.volume = _scoreKeeper.IsToggle("Sound") ? 1.0f : 0.0f;
        else _audioSource.volume = _scoreKeeper.IsToggle("Music") ? 1.0f : 0.0f;
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        if (destroyed && (isMusic && _scoreKeeper.IsToggle("Music") || !isMusic && _scoreKeeper.IsToggle("Sound")))
        {
            AudioSource.PlayClipAtPoint(sound, transform.position, volume);
        }
        else
        {
            _audioSource.PlayOneShot(sound, volume);
        }
    }
    

}
