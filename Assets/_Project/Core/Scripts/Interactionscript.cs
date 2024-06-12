using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactionscript : MonoBehaviour
{
    // Flag to check if is Monster candy
    [SerializeField]
    private bool _ismonsterCandy;
    // Property to get and set candy type
    public bool CandyType
    {
        get { return _ismonsterCandy; }
        set { _ismonsterCandy = value; }
    }

    //Dramatic Organ, A.wav by InspectorJ -- https://freesound.org/s/402095/ -- License: Attribution 4.0
    //buzzer_boardgame_02.wav by magedu -- https://freesound.org/s/395806/ -- License: Attribution 4.0
    //Bad Beep (Incorrect) by RICHERlandTV -- https://freesound.org/s/216090/ -- License: Attribution 4.0
    //Spooky Ghost Sound by TOMRORYPARSONS -- https://freesound.org/s/338095/ -- License: Attribution 3.0
}
