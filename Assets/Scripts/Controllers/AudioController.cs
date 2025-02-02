using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource soundSource;

    public float Sonidito;

    // M�todo para cambiar el volumen de la m�sica


    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp(volume, 0f, 1f);  // Asegura que el volumen est� entre 0 y 1
    }

    // M�todo para cambiar el volumen de los efectos de sonido
    public void SetSoundVolume(float volume)
    {
        soundSource.volume = Mathf.Clamp(volume, 0f, 1f);  // Asegura que el volumen est� entre 0 y 1
    }

    public void PlayAudio(AudioClip music, AudioClip sound)
    {
        if (sound != null)
        {
            // Establece el volumen del soundSource en 0.1 antes de reproducir el sonido
            SetSoundVolume(Sonidito);

            // Asigna el clip de sonido y lo reproduce
            soundSource.clip = sound;
            soundSource.Play();
        }

        if (music != null && musicSource.clip != music)
        {
            StartCoroutine(SwitchMusic(music));  // Cambia la m�sica con la transici�n
        }
    }

    private IEnumerator SwitchMusic(AudioClip music)
    {
        if (musicSource.clip != null)
        {
            while (musicSource.volume > 0)
            {
                musicSource.volume -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            musicSource.volume = 0;
        }

        musicSource.clip = music;
        musicSource.Play();

        while (musicSource.volume < 0.5f)
        {
            musicSource.volume += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}