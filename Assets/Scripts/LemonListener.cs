using UnityEngine;


public class LemonListener : MonoBehaviour
{
    //Просто повесить компонент на Лимона нельзя, так как он вращается вместе с ним и звук играет не с той стороны, с которой должен.
    //А если компонент на камере, то он находится слишком высоко. Из-за этого звуки слышатся неправильно.

    private Transform _lemonTransform;

    #region UnityMethods

    private void Start()
    {
        _lemonTransform = FindObjectOfType<LemonController>().transform;
    }

    private void Update()
    {
        transform.position = _lemonTransform.position;
    }

    #endregion
}