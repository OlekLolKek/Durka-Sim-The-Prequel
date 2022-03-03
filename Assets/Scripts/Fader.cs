using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Fader : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioSource _fadeMusic;
    [SerializeField] private Image _fadeImage;

    #endregion


    #region UnityMethods

    private void Start()
    {
        StartCoroutine(nameof(FadeOutCoroutine));
    }

    #endregion


    #region Methods

    public void StartFade()
    {
        StartCoroutine(nameof(FadeOutCoroutine));
    }

    public void EndFade()
    {
        StartCoroutine(nameof(FadeInCoroutine));
    }

    public void QuitFade()
    {
        StartCoroutine(nameof(FadeAndQuit));
    }

    public void FadeToMainMenu()
    {
        StartCoroutine(nameof(FadeToMainMenuCoroutine));
    }

    private IEnumerator FadeToMainMenuCoroutine()
    {
        Color newColor = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeImage.color.a);
        while (_fadeImage.color.a < 1 || _fadeMusic.volume > 0)
        {
            newColor.a += Time.deltaTime;
            _fadeImage.color = newColor;

            _fadeMusic.volume -= Time.deltaTime;

            yield return 0;
        }
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

    private IEnumerator FadeInCoroutine()
    {
        Color newColor = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeImage.color.a);
        while (_fadeImage.color.a < 1 || _fadeMusic.volume > 0)
        {
            newColor.a += Time.deltaTime;
            _fadeImage.color = newColor;

            _fadeMusic.volume -= Time.deltaTime;

            yield return 0;
        }
        yield return new WaitForSeconds(1.0f);
        ChangeScene();
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color newColor = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeImage.color.a);
        while (_fadeImage.color.a > 0)
        {
            newColor.a -= Time.deltaTime;
            _fadeImage.color = newColor;

            yield return 0;
        }
    }

    private IEnumerator FadeAndQuit()
    {
        Color newColor = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, _fadeImage.color.a);
        while (_fadeImage.color.a < 1 || _fadeMusic.volume > 0)
        {
            newColor.a += Time.deltaTime;
            _fadeImage.color = newColor;

            _fadeMusic.volume -= Time.deltaTime;

            yield return 0;
        }
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }

    private void ChangeScene()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    #endregion
}
