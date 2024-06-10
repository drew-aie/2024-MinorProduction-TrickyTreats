using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    [SerializeField]
    private bool _isMonster = false;

    public bool ChildType
    {
        get { return _isMonster; }
        set { _isMonster = value; }
    }
}
