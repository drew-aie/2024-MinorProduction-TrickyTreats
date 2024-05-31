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
    AudioClip _doorOpen;
    [SerializeField]
    AudioClip _doorClose;
    [SerializeField]
    AudioClip _doorSlam;
    [SerializeField]
    private ChildManager _childInteractions;
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

        _mouseClick = new InputAction(binding: "<Mouse>/leftButton");
        _currentCandyBag = Instantiate(_candyBagPrefab);
        _startPosition = _currentCandyBag.transform.position;
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
                    _audio.clip = _doorOpen;

                    _door.transform.DORotate(new Vector3(0, -90, 0), 1).onComplete = _pointsTimer.StartDecreasing;
                    _audio.Play();
                    _childInteractions.CurrentChild.GetComponent<AudioSource>().Play();
                    // Spawn a new candy bag
                    _currentCandyBag.SetActive(true);

                    _isBagDestroyed = false;
                    _isOpen = true;
                }
                
            }

        }
        if (_isBagDestroyed && _isOpen  && !_isTraumatizable)
        {
            _audio.clip = _doorClose;
            _audio.PlayDelayed(.3f);

            // Close the door
            _door.transform.DORotate(new Vector3(0, -180, 0), 1, RotateMode.Fast);
            _pointsTimer.StopDecreasingPoints();

            _currentCandyBag.SetActive(false);
            _isBagDestroyed = false;
                _isOpen = false;
          
        }
        if (_isBagDestroyed && _isOpen && _isTraumatizable)
        {
            _audio.clip = _doorSlam;
            _audio.Play();
            // Close the door
            _door.transform.DORotate(new Vector3(0, -180, 0), .25f, RotateMode.Fast).onComplete = TraumatizeCamera ;
            _pointsTimer.StopDecreasingPoints();
            _currentCandyBag.SetActive(false);
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

//    Game Sound Effects
//Sonniss - Royalty Free Sound Effects Archive: GameAudioGDC - https://sonniss.com/gameaudiogdc
//PMSFX SAMPLER 2022-2023 - https://www.pmsfx.store/product/pmsfx-sampler-2022/
//FreeSound - https://freesound.org/
//sfxr Generator - https://github.com/grimfang4/sfxr
//Bosca Ceoil - https://boscaceoil.net/
//Bensound - https://www.bensound.com/
//Pixabay - https://pixabay.com/music/
//FilmMusic - https://filmmusic.io/
//Free Music Archive - https://freemusicarchive.org/
}
