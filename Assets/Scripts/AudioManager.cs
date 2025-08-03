using UnityEngine;

public static class AudioManager
{
   
    public static void SetSFXVol(float vol)
    {  

        if (vol < 0 || vol > 1) vol = 1f; // protect it from bad values

        PlayerPrefs.SetFloat("SFXVol", vol);
    }

    public static float GetSFXVol()
    {
        if (!PlayerPrefs.HasKey("SFXVol")) SetSFXVol(1);

        float SFXVolume = PlayerPrefs.GetFloat("SFXVol");

        if (SFXVolume > 1 || SFXVolume < 0) SFXVolume = 1; // protect it from bad values

        return SFXVolume;
    }

    public static void SetMusicVol(float vol)
    {
        if (vol < 0 || vol > 1) vol = 1f; // protect it from bad values

        PlayerPrefs.SetFloat("MusicVol", vol);
    }

    public static float GetMusicVol()
    {
        if (!PlayerPrefs.HasKey("MusicVol")) SetSFXVol(1);

        float MusicVolume = PlayerPrefs.GetFloat("MusicVol");

        if (MusicVolume > 1 || MusicVolume < 0) MusicVolume = 1; // protect it from bad values

        return MusicVolume;
    }

    public static void PlaySFX(AudioSource audioSource, AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx, GetSFXVol());
    }

    public static void PlayMusic(AudioSource audioSource, AudioClip music)
    {
        audioSource.loop = true;
        audioSource.volume = GetMusicVol();
        audioSource.clip = music;
        audioSource.Play();
    }


}
