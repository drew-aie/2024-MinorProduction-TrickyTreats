using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultsScreenBehavior : MonoBehaviour
{
    [SerializeField]
    TMP_Text Results;

    // Start is called before the first frame update
    //void Start()
    //{
    //    _ScoreManager = FindObjectOfType<ScoreManager>();
    //}

    //// Update is called once per frame
    void Update()
    {
        Results.text = "Total Score: " + " <br>"+ScoreManager.Instance.GetPoints.ToString();
    }


    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }



}
