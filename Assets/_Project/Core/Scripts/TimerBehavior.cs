using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehavior : MonoBehaviour
{

    [SerializeField]
    private float _maxTime;
    [SerializeField]
    private float _time;

    
    public float GetTime
    {
        get => _time;
    }

    public float SetTime
    {
        set => _time = value;
    }

    public float MaxTime { get => _maxTime; }



    public float setTime(float time)
    {
         return SetTime = time;
    }

    private void Update()
    {
        SetTime=(_time -= Time.deltaTime);
        Debug.Log(GetTime.ToString("F3"));
    }
}
 