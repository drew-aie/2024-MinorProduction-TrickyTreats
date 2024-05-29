using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsTimer : MonoBehaviour
{
    // Maximum points
    private float _maxPoints = 1000f;
    // total points
    private float _globalpoints;
    private float _localpoints;
    // Points decrease rate per second
    [SerializeField]
    private float _decreaseRate = 50f;
    [SerializeField]
    private CandyInterations _human;
    [SerializeField]
    private CandyInterations _monster;
    [SerializeField]
    private float _maxkids = 10;
    private bool _maxReached = false;
    private float _totalgivencandy;
    private ChildController _childInteractions;
    [SerializeField]
    private DoorInteractionScript _doorInteraction;
    [SerializeField]
    private float _secondstowait = 1f;
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
        _localpoints = Mathf.Clamp(_localpoints, 0, _maxPoints);
        

    }
    private void Update()
    {

    }
    public void StartDecreasing()
    {
        StartCoroutine(DecreasePointsOverTime());
    }
    IEnumerator DecreasePointsOverTime()
    {

        while (_localpoints > 0 && _totalgivencandy < _maxkids)
        {

                // Wait for 1 second
                yield return new WaitForSeconds(_secondstowait);
                // Decrease points
                _localpoints -= _decreaseRate;
                //Debug.Log(_localpoints);

            
        }
    }

    private void StopDecreasingPoints()
    {
        StopCoroutine(DecreasePointsOverTime());
         _localpoints = _maxPoints;
        //Debug.Log(_totalgivencandy);
    }

    public void OnOptionSelected()
    {
        // Call this function when an option is selected
        StopDecreasingPoints();

        _totalgivencandy += _monster.MonsterCandy + _human.HumanCandy;
        //Debug.Log("Monster: " + _monster.MonsterCandy);
        //Debug.Log("Human: " + _human.HumanCandy);
        _globalpoints = Mathf.Clamp(_globalpoints, 0, Mathf.Infinity);
        //Debug.Log("Points: " + _globalpoints);
       
    }
    public void AddPoints()
    {


            _globalpoints += _localpoints;
        
    }
    public void RemovePoints()
    {
        _globalpoints -= _localpoints;
    }
}
