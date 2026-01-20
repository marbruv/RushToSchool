using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Awake()
    {
        // Cargar valores guardados o usar default 0.75f
        float master = PlayerPrefs.GetFloat("volume_master", 0.75f);
        float music = PlayerPrefs.GetFloat("volume_music", 0.75f);
        float sfx = PlayerPrefs.GetFloat("volume_sfx", 0.75f);

        // Inicializar sliders sin disparar eventos
        masterSlider.SetValueWithoutNotify(master);
        musicSlider.SetValueWithoutNotify(music);
        sfxSlider.SetValueWithoutNotify(sfx);

        // Aplicar volumen al AudioMixer desde el inicio
        ApplyVolume("MasterVolume", master);
        ApplyVolume("MusicVolume", music);
        ApplyVolume("SFXVolume", sfx);

        // Suscribirse a cambios de slider
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Aplica volumen al AudioMixer en dB
    void ApplyVolume(string parameterName, float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
        audioMixer.SetFloat(parameterName, dB);
    }

    // Métodos llamados al mover los sliders
    public void SetMasterVolume(float volume)
    {
        ApplyVolume("MasterVolume", volume);
        PlayerPrefs.SetFloat("volume_master", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float volume)
    {
        ApplyVolume("MusicVolume", volume);
        PlayerPrefs.SetFloat("volume_music", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        ApplyVolume("SFXVolume", volume);
        PlayerPrefs.SetFloat("volume_sfx", volume);
        PlayerPrefs.Save();
    }
}
