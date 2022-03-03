using UnityEngine;


public class Door : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioSource _cameraAudioSource;
    [SerializeField] private AudioSource _closeAudioSource;
    [SerializeField] private AudioSource _openAudioSource;
    [SerializeField] private Transform _door;
    [SerializeField] private float _rotationSpeed = 5.0f;
    [SerializeField] private bool _isOpenable = true;

    private Animator _doorAnimator;

    #endregion


    #region UnityMethods

    private void Start()
    {
        _doorAnimator = _door.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isOpenable )
            {
                Open();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isOpenable)
            {
                Close();
            }
        }
    }

    #endregion


    #region Methods

    //This method is public because the door needs to be opened by the level boss. 
    //The door doesn't need to be closed when the boss is killed, so Close is private.
    public void Open()
    {
        _doorAnimator.SetTrigger("Open");
        _openAudioSource.Play();
    }

    private void Close()
    {
        _doorAnimator.SetTrigger("Close");
        _closeAudioSource.Play();
        if (_cameraAudioSource)
        {
            _cameraAudioSource.Play();
        }
    }

    #endregion
}
