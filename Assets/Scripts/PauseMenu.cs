using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class PauseMenu : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private Fader _fader;

    private AudioSource _audioSource;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    #endregion


    #region Methods

    public void OnContinueButtonPressed()
    {
        Click();
        Time.timeScale = 1;
        _audioMixer.SetFloat("MusicVolume", 0);
        _pauseMenuPanel.SetActive(false);
    }

    public void OnMainMenuButtonPressed()
    {
        Click();
        Time.timeScale = 1;
        _pauseMenuPanel.SetActive(false);
        _audioMixer.SetFloat("MusicVolume", 0);
        _fader.FadeToMainMenu();
    }

    public void OnExitGameButtonPressed()
    {
        Click();
        Time.timeScale = 1;
        _pauseMenuPanel.SetActive(false);
        _fader.QuitFade();
    }

    private void Click()
    {
        _audioSource.Play();
    }

    #endregion
}
