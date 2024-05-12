using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildInteractions : MonoBehaviour
{
    // Flag to check if child is a monster
    [SerializeField]
    private bool _isChildMonster;

    // Candy needs of the child
    [SerializeField]
    private float _childCandyNeeds;

    // Property to get and set child type
    public bool ChildType
    {
        get { return _isChildMonster; }
        set { _isChildMonster = value; }
    }
}
