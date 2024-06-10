using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float _globalPoints;
    public static ScoreManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
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
