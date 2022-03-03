using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelEnd : MonoBehaviour
{
    #region Fields

    [SerializeField] private Fader _fader;
    [SerializeField] private float _alphaChangeSpeed;

    #endregion


    #region UnityMethods

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("che");
            _fader.EndFade();
        }
    }

    #endregion
}
