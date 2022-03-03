using UnityEngine;


public class Point : MonoBehaviour
{
    #region Fields

    private PointCounter _pointCounter;
    private AudioSource _audioSource;
    private Renderer _renderer;
    private Collider _collider;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _pointCounter = FindObjectOfType<PointCounter>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AddPoint();
        }
    }

    #endregion


    #region Methods

    private void AddPoint()
    {
        _pointCounter.PlusPoint();
        _audioSource.Play();
        _renderer.enabled = false;
        _collider.enabled = false;
        Destroy(gameObject, _audioSource.clip.length);
    }

    #endregion
}
