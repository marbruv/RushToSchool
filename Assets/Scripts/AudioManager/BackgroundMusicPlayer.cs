using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioSource musicSource;

    [Header("Music Clips")]
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;

    void Start()
    {
        PlayGameplayMusic();
    }

    public void PlayGameplayMusic()
    {
        if (musicSource == null || gameplayMusic == null) return;

        musicSource.clip = gameplayMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        if (musicSource == null || gameOverMusic == null) return;

        musicSource.Stop();
        musicSource.clip = gameOverMusic;
        musicSource.loop = true; // or false if you want it to play once
        musicSource.Play();
    }
}