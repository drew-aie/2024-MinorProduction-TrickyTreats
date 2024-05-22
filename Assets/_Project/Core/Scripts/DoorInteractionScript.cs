using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractionScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _door;
    [SerializeField]
    private bool _isOpen = false;
    [SerializeField]
    private InputAction _mouseClick;
    [SerializeField]
    private GameObject _candyBagPrefab;
    private GameObject _currentCandyBag;
    private bool _isBagDestroyed = false;
    [SerializeField]
    private Camera _mainCamera;

    private void Awake()
    {
        _mouseClick = new InputAction(binding: "<Mouse>/leftButton");
        _currentCandyBag = Instantiate(_candyBagPrefab);
        _currentCandyBag.SetActive(false);
    }
    public bool Open
    {
        get { return _isOpen; }
        set { _isOpen = value; }

    }
    private void OnEnable()
    {
        _mouseClick.Enable();
    }

    private void OnDisable()
    {
        _mouseClick.Disable();
    }

    private void Update()
    {

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.gameObject == _door)
            {

                if (!_isOpen && !_currentCandyBag.activeSelf && _mouseClick.triggered)
                {
                    // Open the door
                    
                    _door.transform.DORotate(new Vector3(0, -90, 0), 1);
                    // Spawn a new candy bag
                    _currentCandyBag.SetActive(true);
                    _isBagDestroyed = false;
                    _isOpen = true;
                }
                
            }

        }
        else if (_isBagDestroyed && _isOpen)
        {
            // Close the door
            _door.transform.DORotate(new Vector3(0, -180, 0), 1, RotateMode.Fast);

            _isBagDestroyed = false;
            _isOpen = false;

        }
    }

    public void DestroyCandyBag()
    {
        // Destroy the current candy bag
        _currentCandyBag.SetActive(false);
        _isBagDestroyed = true;
    }
    public void TraumatizeCamera()
    {
        _mainCamera.GetComponent<CameraShake>().AddTrauma(.5f);
    }
}
