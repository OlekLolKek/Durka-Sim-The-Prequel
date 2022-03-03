using System;
using UnityEngine;


public class Mine : MonoBehaviour
{
    #region Fields

    [SerializeField] private LayerMask _explosionLayers;
    [SerializeField] private int _damage;

    private ParticleSystem _explosionParticles;
    private AudioSource _audioSource;
    private Renderer _renderer;
    private Collider _collider;
    private float _explosionForce = 250.0f;
    private float _explosionRaius = 10.0f;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _explosionParticles = GetComponentInChildren<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    #endregion


    #region Methods

    private void Explode()
    {
        var explosionSphere = Physics.OverlapSphere(transform.position, _explosionRaius, _explosionLayers);
        foreach (var obj in explosionSphere)
        {
            if (obj.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddExplosionForce(_explosionForce, transform.position, _explosionRaius);
            }

            if (obj.TryGetComponent<MyEnemy>(out var enemy))
            {
                enemy.Hurt(_damage);
            }
        }
        Destroy(gameObject, _explosionParticles.main.duration);
        _explosionParticles.Play();
        _audioSource.Play();
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    #endregion
}
