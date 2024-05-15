using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    [SerializeField]
    private bool _isMonster = false;

    private bool _hasMonsterCandy = false;
    private bool _hasHumanCandy = false;

    public bool ChildType
    {
        get { return _isMonster; }
        set { _isMonster = value; }
    }
    /// <returns>True if child given correct candy, false if not</returns>
    public bool GetChoice()
    {
        if (_hasHumanCandy && _hasMonsterCandy) { return false; }
        else if (!_hasMonsterCandy && !_hasHumanCandy) { return false; }
        else if (_isMonster && !_hasMonsterCandy) { return false; }
        else if (!_isMonster && !_hasMonsterCandy) { return true; }

        return true;
    }
}
