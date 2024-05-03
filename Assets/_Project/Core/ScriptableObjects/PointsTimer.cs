using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTimer : MonoBehaviour
{
    // set it to true when gameplay has started, to false when level finished or game paused
    private bool _timerRunning = false;
    public void SetTimerRunning(bool value)
    {
        _timerRunning = value;
    }
    [SerializeField]
    private float _remainingTime = 20.0f;

    private void Update()
    {
        if (_timerRunning)
        {
            _remainingTime -= Time.deltaTime;
        }
    }

    // call from anywhere you want to know how many points exist. Mathf.Clamp() used to not go below 0 or above 3
    // 20-15 seconds = 750 stars, 15-10 seconds = 500, 10-5 seconds = 250, 5-0 seconds = 0
    private int GetPoints()
    {
        return Mathf.Clamp((int)(_remainingTime / 4), 0, 1000);
    }
}
