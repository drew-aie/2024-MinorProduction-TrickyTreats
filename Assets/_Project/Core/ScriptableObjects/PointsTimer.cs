using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointsTimer : MonoBehaviour
{
    // Maximum points
    private float maxPoints = 1000f;
    // total points
    private float globalpoints;
    private float localpoints;
    // Points decrease rate per second
    private float decreaseRate = 1f;
    [SerializeField]
    private CandyInterations _human;
    [SerializeField]
    private CandyInterations _monster;
    [SerializeField]
    private float maxkids = 10;
    private float totalgivencandy;
    private void Start()
    {
        localpoints = maxPoints;
        StartCoroutine(DecreasePointsOverTime());
    }

    IEnumerator DecreasePointsOverTime()
    {
        totalgivencandy += _monster.MonsterCandy + _human.HumanCandy;
        Debug.Log(_monster.MonsterCandy);
        Debug.Log(_human.HumanCandy);
        while (localpoints > 0 && totalgivencandy < maxkids)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);
            // Decrease points
            localpoints -= decreaseRate;

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
        globalpoints += localpoints;
        Debug.Log("Points: " + globalpoints);
        localpoints = maxPoints;
        Debug.Log(totalgivencandy);
    }
}
