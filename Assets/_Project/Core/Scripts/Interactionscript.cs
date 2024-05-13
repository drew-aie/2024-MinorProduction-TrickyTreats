using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactionscript : MonoBehaviour
{
    [SerializeField]
    private bool _ismonsterCandy;

    public bool CandyType
    {
        get { return _ismonsterCandy; }
        set { _ismonsterCandy = value; }
    }


}
