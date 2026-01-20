using UnityEngine;
using UnityEngine.Audio;

public class MusicVolumeSync : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioMixer audioMixer;

    void Start()
    {
        // Forzar volumen al AudioSource en 1 para que el AudioMixer lo controle
        musicSource.volume = 1f;

        // Aplicar valor guardado del AudioMixer
        float musicVol = PlayerPrefs.GetFloat("volume_music", 0.75f);
        float dB = Mathf.Log10(Mathf.Max(musicVol, 0.0001f)) * 20f;
        audioMixer.SetFloat("MusicVolume", dB);

        // Reproducir música
        if (!musicSource.isPlaying)
            musicSource.Play();
    }
}
