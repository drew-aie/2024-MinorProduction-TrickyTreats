using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTimer : MonoBehaviour
{
    public float maxPoints = 1000f; // Maximum points
    public float points; // Current points
    public float decreaseRate = 1f; // Points decrease rate per second

    void Start()
    {
        points = maxPoints;
        StartCoroutine(DecreasePointsOverTime());
    }

    IEnumerator DecreasePointsOverTime()
    {
        while (points > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            points -= decreaseRate; // Decrease points
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
