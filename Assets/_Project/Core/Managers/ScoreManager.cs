using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ScoreManager : MonoBehaviour
{
    private float _globalPoints;
    public static ScoreManager Instance;
    private bool _canReset = false;
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
    public bool CanReset
    {
        get { return _canReset; }
        set { _canReset = value; }
    }
    public void ResetPoints()
    {
        if (_canReset == true)
        {
            SetPoints = 0;
            _canReset = false;
        }
        else
        {
            return;
        }
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
