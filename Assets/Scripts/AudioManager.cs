using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip button;
    public AudioClip push;
    public AudioClip walk;

    public Slider musicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        if (PlayerPrefs.HasKey("SFXValue"))
        {
            SFXSource.volume = PlayerPrefs.GetFloat("SFXValue");
        }
        else
        {
            SFXSource.volume = 1;
            PlayerPrefs.SetFloat("SFXValue", SFXSource.volume);
        }

        if (PlayerPrefs.HasKey("musicValue"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("musicValue");
        }
        else
        {
            musicSource.volume = 1;
            PlayerPrefs.SetFloat("musicValue", musicSource.volume);
        }
        musicSlider.value = musicSource.volume;
        SFXSlider.value = SFXSource.volume;
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.clip = clip;
        SFXSource.Play();
    }
    public void MusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicValue", musicSource.volume);
    }
    public void SFXVolume()
    {
        SFXSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("SFXValue", SFXSource.volume);
    }

}
