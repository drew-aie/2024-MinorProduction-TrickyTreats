using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float _globalPoints;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public float GetPoints
    {
        get { return _globalPoints; }

    }
    public float SetPoints
    {
        set {_globalPoints = value; }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
