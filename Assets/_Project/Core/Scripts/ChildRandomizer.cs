using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChildRandomizer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectArray;

    private void Start()
    {
        GameObject obj = _objectArray[Random.Range(0, _objectArray.Length)];
    }

    public GameObject GetRandom()
    {
        GameObject rand = _objectArray[Random.Range(0, _objectArray.Length)];
        return rand;
    }
}
