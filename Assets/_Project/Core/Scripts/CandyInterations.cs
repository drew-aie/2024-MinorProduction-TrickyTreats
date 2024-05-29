using System.Collections;
using System.Collections.Generic;
using System.Globalization;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class CandyInterations : MonoBehaviour
{

    private InputAction _mouseClick;
    private InputActionMap inputActions;
    private bool _ismouseClicked;
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _mouseDragPhysicsSpeed = 10;
    [SerializeField]
    private float _mouseDragSpeed = .01f;
    [SerializeField]
    private GameObject _giveBaglocation;
    [SerializeField]
    private DoorInteractionScript _doorInteraction;

    private Vector3 _startingPosition;
    [SerializeField]
    private Camera _mainCamera;
    private Vector3 _velocity;
    private PointsTimer _pointsTimer;
    private float _monsterCandyCount = 0;
    private float _humanCandyCount = 0;
    [SerializeField]
    private ChildManager _childInteractions;
    [SerializeField]
    private GameObject _currentChild;
    private Interactionscript _interactionscript;
    
    
    public float MonsterCandy 
    {
        get { return _monsterCandyCount; }
        set { _monsterCandyCount = value; }
    }
    public float HumanCandy
    {
        get { return _humanCandyCount; }
        set { _humanCandyCount = value; }
    }

    public bool IsMouseClicked
    {
        get { return _ismouseClicked; }
        set { _ismouseClicked = value; }
    }
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private void Start()
    {
                _startingPosition = gameObject.GetComponent<Rigidbody>().transform.position;

    }
    private void Awake()
    {
        _interactionscript = GetComponent<Interactionscript>();
        _mouseClick = new InputAction(binding: "<Mouse>/leftButton");

        _rigidbody = GetComponent<Rigidbody>();

        _pointsTimer = FindObjectOfType<PointsTimer>();
        
    }
    
    private void OnEnable()
    {

        _mouseClick.Enable();
        IsMouseClicked = true;
        _mouseClick.performed += EventManagerOnMouseHeld;

    }
    private void OnDisable()
    {

        _mouseClick.performed -= EventManagerOnMouseHeld;
        _ismouseClicked = false;

        _mouseClick.Disable();
    }
    private void EventManagerOnMouseHeld(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject != null && (hit.collider.gameObject.CompareTag("Candy")))
            {
                
                StartCoroutine(DragUpdate(hit.collider.gameObject));

            }

        }
    }
    
    private void Update()
    {

        
        Debug.Log("Mouse Position: " + gameObject.transform.position);
        if (_mouseClick.ReadValue<float>() != 0 && gameObject.transform.position != _startingPosition)
        {
            _rigidbody.useGravity = true;


        }


        
    }
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Surface") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Bowl"))
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            gameObject.transform.position = _startingPosition;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            if (_giveBaglocation != null && _rigidbody.useGravity)
            {
                if (_childInteractions == null)
                {
                    _childInteractions = FindObjectOfType<ChildManager>();
                }
                _doorInteraction.DestroyCandyBag();
                // add a check for if the child is a monster or human and if you gave them the right candy
                if (_interactionscript.CandyType && _childInteractions.CurrentChild.GetComponent<ChildController>().ChildType)
                {
                    _pointsTimer.AddPoints();
                    _doorInteraction.IsTraumatizable = false;
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Monster child Monster candy.");
                    MonsterCandy += 1;
                    Debug.Log(_monsterCandyCount);


                }
                else if (!_interactionscript.CandyType && !_childInteractions.CurrentChild.GetComponent<ChildController>().ChildType)
                {
                    _pointsTimer.AddPoints();
                    _doorInteraction.IsTraumatizable = false;
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Human child Human candy.");
                    HumanCandy += 1;
                    Debug.Log(_humanCandyCount);
                }
                else if (!_interactionscript.CandyType && _childInteractions.CurrentChild.GetComponent<ChildController>().ChildType)
                {
                    _pointsTimer.RemovePoints();
                    _doorInteraction.IsTraumatizable = true;
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Monster child Human candy.");
                    HumanCandy += 1;
                    Debug.Log(_humanCandyCount);
                    //_doorInteraction.TraumatizeCamera();
                    
                }
                else if (_interactionscript.CandyType && !_childInteractions.CurrentChild.GetComponent<ChildController>().ChildType)
                {
                    _pointsTimer.RemovePoints();
                    _doorInteraction.IsTraumatizable = true;
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Human child Monster candy.");
                    MonsterCandy += 1;
                    Debug.Log(_monsterCandyCount);
                    //_doorInteraction.TraumatizeCamera();
                }
            }
            else
            {
                //Debug.Log("GiveBagLocation is already null");
            }
        }
    }
    

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        
        float _initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out Rigidbody rb);
        
        while (_mouseClick.ReadValue<float>() != 0) 
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 _direction = ray.GetPoint(_initialDistance) - clickedObject.transform.position;
                rb.velocity = _direction * _mouseDragPhysicsSpeed;

                Vector3 _clickedObjectz = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, Mathf.Clamp(clickedObject.transform.position.z, _startingPosition.z, _startingPosition.z));

                clickedObject.transform.position = Vector3.SmoothDamp(new Vector3(clickedObject.transform.position.x,clickedObject.transform.position.y,_startingPosition.z), ray.GetPoint(_initialDistance), ref _velocity, _mouseDragSpeed);
                clickedObject.transform.position = new Vector3(clickedObject.transform.position.x, clickedObject.transform.position.y, Mathf.Clamp(clickedObject.transform.position.z, _startingPosition.z, _startingPosition.z)); ;


                Debug.DrawRay(ray.origin,ray.direction,Color.red);
                yield return _waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, ray.GetPoint(_initialDistance), ref _velocity, _mouseDragSpeed);
                yield return null;
            }
        }
    }
}
