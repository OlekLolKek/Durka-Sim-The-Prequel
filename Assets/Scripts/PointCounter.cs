using UnityEngine;
using UnityEngine.UI;


public class PointCounter : MonoBehaviour
{
    #region Fields

    [SerializeField] private string _pointsTextName;

    private Text _pointsText;
    private int _points = 0;

    #endregion


    #region UnityMethods

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        _pointsText = GameObject.Find(_pointsTextName).GetComponent<Text>();
        _pointsText.text = $"POINTS: {_points}";
    }

    private void OnLevelWasLoaded(int level)
    {
        _pointsText = GameObject.Find(_pointsTextName).GetComponent<Text>();
        _pointsText.text = $"POINTS: {_points}";
    }

    #endregion


    #region Methods

    public void PlusPoint()
    {
        _points++;
        _pointsText.text = $"POINTS: {_points}";
    }

    #endregion
}
