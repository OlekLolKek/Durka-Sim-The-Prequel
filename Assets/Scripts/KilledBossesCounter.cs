using UnityEngine;
using System.Collections;


public class KilledBossesCounter : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Door _bossDoor;

    private float _musicFadeMultiplier = 0.5f;
    private int _bossesKilled = 0;
    private int _bossesAmount = 3;

    #endregion


    #region Methods

    public void BossKilled()
    {
        _bossesKilled++;
        if (_bossesKilled == _bossesAmount)
        {
            StartCoroutine(nameof(ReduceVolume));
            _bossDoor.Open();
        }
    }

    private IEnumerator ReduceVolume()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime * _musicFadeMultiplier;
            yield return new WaitForSeconds(0.007f);
        }
    }

    #endregion
}
