using UnityEngine;


public class LightFlicker : MonoBehaviour
{
    #region Fields

    private Light _light;
    private ParticleSystem _particles;
    private AudioSource _audioSource;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _light = GetComponent<Light>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _particles = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (Time.frameCount % 3 == 0)
        {
            Flicker();
        }
    }

    #endregion


    #region Methods

    private void Flicker()
    {
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0: 
                _light.enabled = false; 
                break;
            case 1: 
                _light.enabled = true;
                if (_particles)
                {
                    _particles.Play();
                }
                if (_audioSource)
                {
                    _audioSource.Play();
                }
                break;
        }
    }

    #endregion
}
