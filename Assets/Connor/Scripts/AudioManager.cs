using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunAudioSource;
    public AudioSource punchAudioSource;

    public void PlayGunSound()
    {
        gunAudioSource.Play();
    }

    public void PlayPunchSound()
    {
        punchAudioSource.Play();
    }
}