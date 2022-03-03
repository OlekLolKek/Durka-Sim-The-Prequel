using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class LemonController : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private AudioClip _pointSound;
    [SerializeField] private Image _gameEndPicture;
    [SerializeField] private Image _boostBar;
    [SerializeField] private Text _pointsText;
    [SerializeField] private Text _boostText;
    [SerializeField] private float _boostDuration = 3.0f;
    [SerializeField] private float _boostedSpeed = 3.0f;
    [SerializeField] private float _normalSpeed = 1.5f;
    [SerializeField] private float _rotateSpeed = 1.0f;

    private AudioSource _audioSource;
    private Animator _animator;
    private Vector3 _lemonDirection;
    private Color _spriteColor;
    private float _alphaChangeSpeed = 1.0f;
    private float _reloadTime = 1.0f;
    private float _boostTimer = 0.0f;
    private bool _isBoosted = false;
    private bool _isAlive = true;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _boostBar.fillAmount = 0;
        _animator = GetComponent<Animator>();
        _animator.speed = _normalSpeed;
        _spriteColor = new Color(_gameEndPicture.color.r, _gameEndPicture.color.g, _gameEndPicture.color.b, 0.0f);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Move();

        PauseCheck();

        if (_isBoosted)
        {
            BoostUpdate();
        }
    }

    #endregion


    #region Methods

    public void Boost()
    {
        _boostTimer = _boostDuration;
        _isBoosted = true;
        _animator.speed = _boostedSpeed;
    }

    public void Die()
    {
        if (_isAlive)
        {
            _isAlive = false;
            StartCoroutine(nameof(EnablePicture));
            _animator.SetFloat("Speed", 0.0f);
        }
    }

    private IEnumerator EnablePicture()
    {
        while (_spriteColor.a < 1)
        {
            _spriteColor.a += Time.deltaTime * _alphaChangeSpeed;
            _gameEndPicture.color = _spriteColor;
            yield return new WaitForSeconds(0.007f);
        }
        Invoke(nameof(ReloadScene), _reloadTime);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Move()
    {
        if (_isAlive)
        {
            _lemonDirection.x = Input.GetAxis("Horizontal");
            _lemonDirection.y = 0;
            _lemonDirection.z = Input.GetAxis("Vertical");
            _lemonDirection.Normalize();

            Vector3 movement = Vector3.RotateTowards(transform.forward, _lemonDirection, _rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(movement);
            _animator.SetFloat("Speed", _lemonDirection.magnitude);

            if (_lemonDirection.magnitude >= 0.1f)
            {
                if (Time.timeScale > 0)
                {
                    if (!_audioSource.isPlaying)
                    {
                        _audioSource.Play();
                    }
                }
            }
            else
            {
                _audioSource.Stop();
            }
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void Unboost()
    {
        _boostText.text = "";
        _boostTimer = _boostDuration;
        _isBoosted = false;
        _animator.speed = _normalSpeed;
    }

    private void BoostUpdate()
    {
        _boostTimer -= Time.deltaTime;
        _boostText.text = string.Format("{0:f2}", _boostTimer);
        _boostBar.fillAmount = _boostTimer / _boostDuration;
        if (_boostTimer <= 0)
        {
            Unboost();
        }
    }

    private void PauseCheck()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchPause();
        }
    }

    private void SwitchPause()
    {
        if (Time.timeScale == 0)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        _audioSource.Stop();
        _audioMixer.SetFloat("MusicVolume", -40);
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void Unpause()
    {
        _audioMixer.SetFloat("MusicVolume", 0);
        _pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }    

    #endregion
}
