using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD
using TMPro;
using DG.Tweening;

=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)

public class PointsTimer : MonoBehaviour
{
    // Maximum points
    private float _maxPoints = 1000f;
    // total points
<<<<<<< HEAD
<<<<<<< HEAD
    int value = 0;
=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
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
<<<<<<< HEAD
<<<<<<< HEAD
        DOTween.To(() => value, (x) => value = x, 25, 5).SetRelative().SetEase(Ease.InOutQuad);
        //_localpoints = Mathf.Clamp(_localpoints, 0, _maxPoints);

=======
        _localpoints = Mathf.Clamp(_localpoints, 0, _maxPoints);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
        _localpoints = Mathf.Clamp(_localpoints, 0, _maxPoints);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
        

    }
    private void Update()
    {
<<<<<<< HEAD
<<<<<<< HEAD

        _Time.text = _localpoints.ToString();

=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)

    }
    public void StartDecreasing()
    {
        
        StartCoroutine(DecreasePointsOverTime());
        
    }
    public IEnumerator DecreasePointsOverTime()
    {

        while (_localpoints > 0 && _totalgivencandy < _maxkids)
        {

                // Wait for 1 second
<<<<<<< HEAD
<<<<<<< HEAD
                yield return new WaitForSeconds(0);
=======
                yield return new WaitForSeconds(_secondstowait);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
                yield return new WaitForSeconds(_secondstowait);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
                // Decrease points
                _localpoints -= _decreaseRate;
                Debug.Log(_localpoints);

            
        }
    }

    private void StopDecreasingPoints()
    {
        StopCoroutine(DecreasePointsOverTime());
<<<<<<< HEAD
<<<<<<< HEAD
        //_localpoints = _maxPoints;
        Debug.Log("t=" + _totalgivencandy);
=======
         _localpoints = _maxPoints;
        //Debug.Log(_totalgivencandy);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
=======
         _localpoints = _maxPoints;
        //Debug.Log(_totalgivencandy);
>>>>>>> parent of 98ac4c7 (added a working points timer and sound effects)
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
