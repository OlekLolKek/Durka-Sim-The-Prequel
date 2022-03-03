using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Titres : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _fadeImage;
    [SerializeField] private Fader _fader;
    [SerializeField] private float _movingSpeed = 5.0f;

    private AudioSource _musicSource;

    #endregion


    #region UnityMethods

    private void Start()
    {
        StartCoroutine(nameof(MoveUp));
        _musicSource = Camera.main.GetComponent<AudioSource>();
    }

    #endregion


    #region Methods

    private IEnumerator MoveUp()
    {
        while (transform.position.y < 75)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * _movingSpeed, transform.position.z);
            yield return 0;
        }
        yield return new WaitForSeconds(3.5f);
        _fader.EndFade();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }

    #endregion
}
