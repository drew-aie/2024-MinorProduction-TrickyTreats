using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuBehavior : MonoBehaviour
{
    public static bool GamePaused = false;

    public GameObject PauseMenuUI;

    private float _previousTimeScale = Time.timeScale;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        
        {
            if (GamePaused==false)
            {
                Debug.Log("resume");
                Resume();
            }
            else
            {
                Debug.Log("Pause");

                Pause();
            }
        } 
        
    }

    void Resume()
    {
        Debug.Log("function resume");
        PauseMenuUI.SetActive(false);
        Time.timeScale = _previousTimeScale;
        GamePaused = false;
    }

    void Pause()
    {
        Debug.Log("function Pause");
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.000001f;
        GamePaused = true;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (!GamePaused)
            Pause();

        else
            Resume();
    }
}
