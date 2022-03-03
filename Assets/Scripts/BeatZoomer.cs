using UnityEngine;


public class BeatZoomer : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _shakeMultiplier = 2.0f;

    private AudioSource _audioSource;
    private Camera _camera;
    private float _defaultFOV;
    private float _newFOV;
    private float[] _samples = new float[64];

    #endregion


    #region UnityMethods

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _audioSource = GetComponent<AudioSource>();
        _defaultFOV = _camera.fieldOfView;
    }

    private void Update()
    {
        _audioSource.GetSpectrumData(_samples, 1, FFTWindow.BlackmanHarris);
        _newFOV = _defaultFOV;
        for (int i = 0; i < 3; i++)
        {
            _newFOV -= _samples[i] * _shakeMultiplier;
        }
        _camera.fieldOfView = _newFOV;
    }

    #endregion
}
