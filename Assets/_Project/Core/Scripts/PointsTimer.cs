using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using DG.Tweening;



public class PointsTimer : MonoBehaviour
{
    // Text field for displaying time
    [SerializeField]
    private TMP_Text _Time;
    [SerializeField]
    private TMP_Text _TotalPoints;
    [SerializeField]
    private TMP_Text _Kids;
    [SerializeField]
    // Maximum points

    private float _maxPoints = 1000f;
    // total points
    int value = 0;
    private float _globalpoints;
    private float _localpoints;
    // Points decrease rate per second
    private float _decreaseRate;
    [SerializeField]
    private CandyInterations _human;
    [SerializeField]
    private CandyInterations _monster;
    [SerializeField, Min(2)]
    private int _maxkids = 10;
    private bool _maxReached = false;
    private float _totalgivencandy;
    private ChildController _childInteractions;
    [SerializeField]
    private DoorInteractionScript _doorInteraction;
    [SerializeField]
    private float _secondstowait = 1f;
    private bool _stopDecreasing = false;
    [SerializeField]
    private Gradient _timerGradient;

    public bool MaxReached()
    {
        if (_totalgivencandy >= _maxkids)
        {
            _maxReached = true;
        }
        return _maxReached;
    }

    private void Start()
    {
        _childInteractions = FindObjectOfType<ChildController>();
        _localpoints = _maxPoints;
        DOTween.To(() => value, (x) => value = x, 25, 10).SetRelative().SetEase(Ease.OutExpo);
        //_localpoints = Mathf.Clamp(_localpoints, 0, _maxPoints);
        _totalgivencandy = Mathf.Clamp(_totalgivencandy, 0, _maxkids);
        
    }
    private void Update()
    {

        _Kids.text = _totalgivencandy.ToString() + "/" + _maxkids ;
        if (_stopDecreasing)
        {
            _localpoints = 0;
            _Time.text = "0";
            StopDecreasingPoints();
        }
        else if (!_stopDecreasing && _localpoints <= 0)
        {
            _localpoints = 0;
            _Time.text = "0";
        }
        float _timePercentage = Mathf.Clamp01(_localpoints / _maxPoints);
        _Time.color = _timerGradient.Evaluate(_timePercentage);
        _Time.text = _localpoints.ToString();
        _TotalPoints.text = _globalpoints.ToString();
        if (_totalgivencandy == _maxkids)
        {
            _doorInteraction.GetComponent<Collider>().enabled = false;
            _Kids.text = "<s>" + "Children in " + "<br>" + "Neighbourhood: " + "<br>" + _totalgivencandy.ToString() + "/" + _maxkids + "<s>";
            StopDecreasingPoints();
        }

    }
    public void StartDecreasing()
    {
        _stopDecreasing = false;
        StartCoroutine(DecreasePointsOverTime());
        
    }
    IEnumerator DecreasePointsOverTime()
    {

        while ( _totalgivencandy < _maxkids)
        {

                // Wait for 1 second
                yield return new WaitForSeconds(0.05f);
                // Decrease points
                _localpoints -= value;

                Debug.Log(_localpoints);

            
        }

    }

    public void StopDecreasingPoints()
    {
        _stopDecreasing = true;
        StopCoroutine(DecreasePointsOverTime());
        _localpoints = _maxPoints;
        Debug.Log("t=" + _totalgivencandy);
    }

    public void OnOptionSelected()
    {
        // Call this function when an option is selected
        StopDecreasingPoints();
        //_totalgivencandy = _monster.MonsterCandy + _human.HumanCandy;
        //Debug.Log("Monster: " + _monster.MonsterCandy);
        //Debug.Log("Human: " + _human.HumanCandy);
        _globalpoints = Mathf.Clamp(_globalpoints, 0, Mathf.Infinity);
        //Debug.Log("Points: " + _globalpoints);
       
    }
    public void AddPoints()
    {
        if (_totalgivencandy < _maxkids)
        {
            _globalpoints += _localpoints;
            _totalgivencandy++;
        }

    }
    public void RemovePoints()
    {
        if (_totalgivencandy < _maxkids)
        {
            _globalpoints -= _localpoints;
            _totalgivencandy++;
        }
    }
}
