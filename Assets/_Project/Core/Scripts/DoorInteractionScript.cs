using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
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
    private GameObject _bagLocation;
    private Vector3 _startPosition;
    [SerializeField]
    private GameObject _candyBagPrefab;
    private GameObject _currentCandyBag;
    private bool _isBagDestroyed = false;
    private bool _isTraumatizable;
    [SerializeField]
    private PointsTimer _pointsTimer;
    AudioSource _audio;
    [SerializeField]
    private AudioClip _OpenSlowly;
    [SerializeField]
    private AudioClip _CloseSlowly;
    [SerializeField]
    private AudioClip _Slam;
    public bool IsTraumatizable
    {
        get { return _isTraumatizable; }
        set { _isTraumatizable = value; }
    }
        [SerializeField]
    private Camera _mainCamera;
    public bool CandyBagActive
    {
        get { return _isBagDestroyed; }
    }
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _audio.playOnAwake = false;
        GetComponent<Collider>().enabled = true;
        _mouseClick = new InputAction(binding: "<Mouse>/leftButton");
        _currentCandyBag = Instantiate(_candyBagPrefab);
        _startPosition = _currentCandyBag.transform.position;
        _currentCandyBag.GetComponent<MeshRenderer>().enabled = false;
    }
    private void Start()
    {
        GetComponent<Collider>().enabled = true;
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

                if (!_isOpen && !_currentCandyBag.GetComponent<MeshRenderer>().enabled && _mouseClick.triggered)
                {
                    // Open the door
                    _audio.clip = _OpenSlowly;
                    _audio.Play();
                    _door.transform.DORotate(new Vector3(0, -90, 0), 1).onComplete = _pointsTimer.StartDecreasing;
                    // Spawn a new candy bag
                    _currentCandyBag.GetComponent<MeshRenderer>().enabled = true;
                    _isBagDestroyed = false;
                    _isOpen = true;
                }
                
            }

        }
        if (_isBagDestroyed && _isOpen  && !_isTraumatizable)
        {
            _audio.clip = _CloseSlowly;
            _audio.Play();

            // Close the door
            _door.transform.DORotate(new Vector3(0, -180, 0), 1, RotateMode.Fast);
            _currentCandyBag.GetComponent<MeshRenderer>().enabled = false;
            _isBagDestroyed = false;
                _isOpen = false;
          
        }
        if (_isBagDestroyed && _isOpen && _isTraumatizable)
        {
            _audio.clip = _Slam;
            _audio.Play();
            // Close the door
            _door.transform.DORotate(new Vector3(0, -180, 0), .25f, RotateMode.Fast).onComplete = TraumatizeCamera ;
            _currentCandyBag.GetComponent<MeshRenderer>().enabled = false;
            _isBagDestroyed = false;
            _isOpen = false;

        }
    }

    public void DestroyCandyBag()
    {
        // Destroy the current candy bag
        _currentCandyBag.transform.DOMove(_startPosition, 1f);
        
        _isBagDestroyed = true;
    }
    public void TraumatizeCamera()
    {
        
            _mainCamera.GetComponent<CameraShake>().AddTrauma(.5f);
        
    }

}
