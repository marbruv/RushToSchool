using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance; // Acceso f�cil dentro de la escena

    public AudioSource audioSource;

    [Header("Clips de sonido de esta escena")]
    public AudioClip clickSound;
    public AudioClip speedSound;
    public AudioClip crashSound;
    public AudioClip ringBell;
    public AudioClip upgradeSound;

    void Awake()
    {
        // Singleton dentro de la escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evita duplicados dentro de la misma escena
            return;
        }

        Instance = this;

        // No usamos DontDestroyOnLoad para que se destruya al cambiar de escena
    }

    // M�todos para reproducir sonidos
    public void PlayClick() { PlaySound(clickSound); }
    public void PlayUpgrade() { PlaySound(upgradeSound); }
    public void PlaySpeed() { PlaySound(speedSound); }
    public void PlayCrash() { PlaySound(crashSound); }
    public void PlayRingBell() { PlaySound(ringBell); }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }
}
