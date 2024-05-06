using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTimer : MonoBehaviour
{
    // Maximum points
    public float maxPoints = 1000f;
    // Current points
    public float points;
    // Points decrease rate per second
    public float decreaseRate = 1f; 

    void Start()
    {
        points = maxPoints;
        StartCoroutine(DecreasePointsOverTime());
    }

    IEnumerator DecreasePointsOverTime()
    {
        while (points > 0)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
            // Decrease points
            points -= decreaseRate; 
        }
    }

    public void StopDecreasingPoints()
    {
        StopCoroutine(DecreasePointsOverTime());
    }

    public void OnOptionSelected()
    {
        // Call this function when an option is selected
        StopDecreasingPoints();
        Debug.Log("Points: " + points);
    }
}
