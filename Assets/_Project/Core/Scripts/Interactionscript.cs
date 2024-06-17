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
}
