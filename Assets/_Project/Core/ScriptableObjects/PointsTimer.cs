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
    private float _decreaseRate = 1f;
    [SerializeField]
    private CandyInterations _human;
    [SerializeField]
    private CandyInterations _monster;
    [SerializeField]
    private float _maxkids = 3;
    private bool _maxReached = false;
    private float _totalgivencandy;
    [SerializeField]
    private ChildInteractions _childInteractions;

    public bool MaxReached()
    {
        if (_totalgivencandy == _maxkids)
        {
            _maxReached = true;
        }
        return _maxReached;
    }
    private void Awake()
    {

    }
    private void Start()
    {

        _localpoints = _maxPoints;
        StartCoroutine(DecreasePointsOverTime());
    }

    IEnumerator DecreasePointsOverTime()
    {
        _totalgivencandy += _monster.MonsterCandy + _human.HumanCandy;
        Debug.Log(_monster.MonsterCandy);
        Debug.Log(_human.HumanCandy);
        while (_localpoints > 0 && _totalgivencandy < _maxkids)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
            // Decrease points
            _localpoints -= _decreaseRate;

        }
    }

    private void StopDecreasingPoints()
    {
        StopCoroutine(DecreasePointsOverTime());
    }

    public void OnOptionSelected()
    {
        // Call this function when an option is selected
        StopDecreasingPoints();
        

        Debug.Log("Points: " + _globalpoints);
        _localpoints = _maxPoints;
        Debug.Log(_totalgivencandy);
    }
    public void Totalpointsmath(ChildInteractions interaction, bool candy)
    {
        if (interaction.ChildType && candy)
        {
            Debug.Log(_human.IsMonster);
            Debug.Log(_monster.IsMonster);
            _globalpoints += _localpoints;
        }
        else if (interaction.ChildType && !candy)
        {
            _globalpoints -= _localpoints;

        }
    }
}
