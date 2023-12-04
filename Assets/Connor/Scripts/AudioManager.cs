using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunAudioSource;
    public AudioSource punchAudioSource;
    //public AudioSource soundTrackAudioSource;

    public void PlayGunSound()
    {
        gunAudioSource.Play();
    }

    public void PlayPunchSound()
    {
        punchAudioSource.Play();
    }
    
    /* public void PlaySoundtrack()
    {
        soundTrackAudioSource.Play();
    } */
}