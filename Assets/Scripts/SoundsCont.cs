using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundsCont : MonoBehaviour
{
    public AudioClip[] _sounds;
    protected AudioSource _audioSource => GetComponent<AudioSource>();

    protected void PlaySound(AudioClip sound, float volume = 1.0f, bool destroyed = false)
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(sound, transform.position, volume);
        }
        else
        {
            _audioSource.PlayOneShot(sound, volume);
        }
    }
    

}
