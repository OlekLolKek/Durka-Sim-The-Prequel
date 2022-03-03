using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MainMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioSource _wooshAudioSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _SFXSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Fader _fader;

    private Animator _cameraAnimator;
    private AudioSource _audioSource;
    private string _musicMixerVolume = "MusicVolume";
    private string _SFXMixerVolume = "SFXVolume";

    #endregion


    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _cameraAnimator = Camera.main.GetComponent<Animator>();
        _audioMixer.GetFloat(_musicMixerVolume, out var music);
        _musicSlider.value = music;
        _audioMixer.GetFloat(_SFXMixerVolume, out var sfx);
        _SFXSlider.value = sfx;
    }

    #endregion


    #region Methods

    public void OnStartGameButtonPressed()
    {
        _fader.EndFade();
        Click();
    }

    public void OnOptionsButtonPressed()
    {
        _cameraAnimator.SetTrigger("OptionsButtonPressed");
        _wooshAudioSource.Play();
        Click();
    }

    public void OnExitButtonPressed()
    {
        Click();
        _fader.QuitFade();
    }

    public void OnBackOptionsButtonPressed()
    {
        _cameraAnimator.SetTrigger("BackOptionsButtonPressed");
        _wooshAudioSource.Play();
        Click();
    }

    public void OnMusicSliderVolumeChanged()
    {
        _audioMixer.SetFloat("MusicVolume", _musicSlider.value);
    }

    public void OnSFXSliderVolumeChanged()
    {
        _audioMixer.SetFloat("SFXVolume", _SFXSlider.value);
    }

    private void Click()
    {
        _audioSource.Play();
    }

    #endregion
}
