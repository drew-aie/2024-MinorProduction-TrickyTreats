using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;
    [SerializeField]
    private bool _isOpenable = true;
    [SerializeField]
    private InputAction _mouseClick; // Define the InputAction

    private void Awake()
    {
        _mouseClick = new InputAction(binding: "<Mouse>/leftButton"); // Initialize the InputAction
    }

    private void OnEnable()
    {
        _mouseClick.Enable(); // Enable the InputAction
    }

    private void OnDisable()
    {
        _mouseClick.Disable(); // Disable the InputAction
    }

    private void Update()
    {
        if (_mouseClick.triggered)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == _gameObject)
                {
                    // Check if the CandyBag GameObject exists in the scene
                    GameObject candyBag = GameObject.FindGameObjectWithTag("Finish");

                    // Only allow the door to open if _isOpenable is true and the CandyBag GameObject does not exist
                    if (_isOpenable && candyBag != null)
                    {
                        // Open the door
                        _gameObject.transform.DORotate(new Vector3(0, -90, 0), 1);
                    }
                    // Allow the door to close if _isOpenable is false and the mouse button is pressed
                    else if (_isOpenable && candyBag == null)
                    {
                        // Close the door
                        _gameObject.transform.DORotate(new Vector3(0, 0, 0), 1);
                        _isOpenable = true;
                    }
                }
            }
        }
    }
}
