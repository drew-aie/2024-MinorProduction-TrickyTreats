using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIBehavior : MonoBehaviour
{
    [SerializeField]
    private RawImage _timerBarFG;

    [SerializeField]
    private TimerBehavior _timerBehavior;

    [SerializeField]
    private Gradient _timerBarGradient;

    //TimerBehavior.Time

    private void Update()
    {
        //guardclause
        if (_timerBarFG == null || _timerBehavior == null)
            return;

        float time = _timerBehavior.GetTime;

        float maxTime = Mathf.Max(0.001f, _timerBehavior.MaxTime);

        //prevents dividing by 0
        float timePercentage = Mathf.Clamp01(time / maxTime);

        

        Vector3 newScale = _timerBarFG.rectTransform.localScale;
        newScale.x = timePercentage;
        _timerBarFG.rectTransform.localScale = newScale;

        _timerBarFG.color = _timerBarGradient.Evaluate(timePercentage);
        
    }

}
