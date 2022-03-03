using UnityEngine;


public class Booster : MonoBehaviour
{
    #region Fields

    private Renderer _renderer;
    private Collider _collider;
    private AudioSource _audioSource;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Boost(other);
        }
    }

    #endregion


    #region Methods

    private void Boost(Collider other)
    {
        other.GetComponent<LemonController>().Boost();
        _audioSource.Play();
        Destroy(gameObject, _audioSource.clip.length);
        _collider.enabled = false;
        _renderer.enabled = false;
    }

    #endregion
}
