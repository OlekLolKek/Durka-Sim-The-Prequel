using UnityEngine;
using UnityEngine.UI;


public class Spawner : MonoBehaviour
{
    #region Fields

    [SerializeField] private Image _reloadIndicator;
    [SerializeField] private GameObject _mine;

    private AudioSource _audioSource;
    private float _reloadTime = 2.5f;
    private float _reloadingTimer = 0.0f;
    private bool _isReadyToFire = true;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _reloadIndicator.fillAmount = 0.0f;
        _isReadyToFire = true;
    }

    private void Update()
    {
        FireRaycast();
    }

    #endregion


    #region Methods

    private void StartReload()
    {
        _reloadIndicator.fillAmount = 0.0f;
        _isReadyToFire = false;
        _reloadingTimer = 0.0f;
    }

    private void FireRaycast()
    {
        if (!_isReadyToFire)
        {
            ReloadCycle();
        }
        if (Time.timeScale == 1)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (Physics.Raycast(ray, out var hit))
            {
                if (_isReadyToFire)
                {
                    if (hit.transform.gameObject.CompareTag("Ground"))
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            Instantiate(_mine, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                            _audioSource.Play();
                            StartReload();
                        }
                    }
                }
            }
        }
    }

    private void ReloadCycle()
    {
        _reloadingTimer += Time.deltaTime;
        _reloadIndicator.fillAmount = _reloadingTimer / _reloadTime;
        if (_reloadingTimer >= _reloadTime)
        {
            _isReadyToFire = true;
            _reloadIndicator.fillAmount = 0.0f;
        }
    }

    #endregion
}
