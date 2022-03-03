using UnityEngine;


public class FireplaceFire : MonoBehaviour
{
    #region Fields

    private Light _light;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _light = GetComponent<Light>();
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
        int rnd = Random.Range(0, 5);
        switch (rnd)
        {
            case 0: _light.intensity = 0.75f; break;
            case 1: _light.intensity = 1.0f; break;
            case 2: _light.intensity = 1.25f; break;
            case 3: _light.intensity = 1.5f; break;
            case 4: _light.intensity = 1.75f; break;
        }
    }

    #endregion
}
