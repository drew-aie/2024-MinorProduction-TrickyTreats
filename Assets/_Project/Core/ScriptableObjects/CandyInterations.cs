using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CandyInterations : MonoBehaviour
{

    // Input action for mouse click
    private InputAction _mouseClick;

    // Rigidbody of the game object
    private Rigidbody _rigidbody;

    // Speed of mouse drag physics
    [SerializeField]
    private float _mouseDragPhysicsSpeed = 10;

    // Speed of mouse drag
    [SerializeField]
    private float _mouseDragSpeed = .1f;

    // Location to give bag
    [SerializeField]
    private GameObject _giveBaglocation;

    // Script for door interaction
    [SerializeField]
    private DoorInteractionScript _doorInteraction;

    // Flag to check if candy is monster candy
    [SerializeField]
    private bool _isMonsterCandy;

    // Starting position of the game object
    private Vector3 _startingPosition;

    // Main camera of the game
    private Camera _mainCamera;

    // Velocity of the game object
    private Vector3 _velocity;

    // Timer for points
    private PointsTimer _pointsTimer;

    // Count of monster candy
    private float _monsterCandyCount = 0;

    // Count of human candy
    private float _humanCandyCount = 0;

    // Script for child interactions
    private ChildInteractions _childInteractions;

    // Script for interaction
    private Interactionscript _interactionscript;

    // Property for monster candy count
    public float MonsterCandy
    {
        get { return _monsterCandyCount; }
        set { _monsterCandyCount = value; }
    }

    // Property for human candy count
    public float HumanCandy
    {
        get { return _humanCandyCount; }
        set { _humanCandyCount = value; }
    }

    // Property to check if candy is monster candy
    public bool IsMonster
    {
        get { return _isMonsterCandy; }
    }

    // Wait for fixed update
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

    // Awake function to initialize variables
    private void Awake()
    {
        _interactionscript = FindObjectOfType<Interactionscript>();
        _mouseClick = new InputAction(binding: "<Mouse>/leftButton");
        _velocity = Vector3.zero;
        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _startingPosition = gameObject.transform.position;
        _pointsTimer = FindObjectOfType<PointsTimer>();
        _childInteractions = FindObjectOfType<ChildInteractions>();
    }

    // Enable mouse click input action
    private void OnEnable()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;

    }

    // Disable mouse click input action
    private void OnDisable()
    {
        _mouseClick.performed -= MousePressed;
        _rigidbody.useGravity = true;
        _mouseClick.Disable();
    }

    // Function to handle mouse press event
    public void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && (hit.collider.gameObject.CompareTag("Candy")))
            {

                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }

        }


    }

    // Update function to handle gravity
    private void Update()
    {
        if (_mouseClick.ReadValue<float>() != 0)
        {
            _rigidbody.useGravity = true;

        }
    }

    // Function to handle collision enter event
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Surface") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Candy"))
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            gameObject.transform.position = _startingPosition;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            if (_giveBaglocation != null && _rigidbody.useGravity)
            {
                _doorInteraction.DestroyCandyBag();
                // add a check for if the child is a monster or human and if you gave them the right candy
                if (_interactionscript.CandyType && _childInteractions.ChildType)
                {
                    _pointsTimer.Totalpointsmath(_childInteractions, IsMonster);
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Monster child Monster candy.");
                    MonsterCandy += 1;
                    Debug.Log(_monsterCandyCount);


                }
                else if (!_interactionscript.CandyType && !_childInteractions.ChildType)
                {
                    _pointsTimer.Totalpointsmath(_childInteractions, IsMonster);

                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Human child Human candy.");
                    HumanCandy += 1;
                    Debug.Log(_humanCandyCount);
                }
                else if (_interactionscript.CandyType && !_childInteractions.ChildType)
                {
                    _pointsTimer.Totalpointsmath(_childInteractions, IsMonster);

                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Monster child Human candy.");
                    HumanCandy += 1;
                    Debug.Log(_humanCandyCount);
                    _doorInteraction.TraumatizeCamera();

                }
                else if (!_interactionscript.CandyType && _childInteractions.ChildType)
                {
                    _pointsTimer.Totalpointsmath(_childInteractions, IsMonster);
                    _pointsTimer.OnOptionSelected();
                    Debug.Log("You gave the Human child Monster candy.");
                    MonsterCandy += 1;
                    Debug.Log(_monsterCandyCount);
                    _doorInteraction.TraumatizeCamera();
                }
            }
            else
            {
                //Debug.Log("GiveBagLocation is already null");
            }
        }
    }

    // Coroutine to handle drag update
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
