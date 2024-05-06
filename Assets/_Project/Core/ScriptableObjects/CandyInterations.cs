using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CandyInterations : MonoBehaviour
{
    [SerializeField]
    private InputAction _mouseClick;
    private Rigidbody _rigidbody;
    [SerializeField]
    private float _mouseDragPhysicsSpeed = 10;
    [SerializeField]
    private float _mouseDragSpeed = .1f;
    [SerializeField]
    private GameObject _giveBaglocation;
    [SerializeField]
    private bool _isMonsterCandy;
    private Vector3 _startingPosition;
    private Camera _mainCamera;
    private Vector3 _velocity = Vector3.zero;
    private PointsTimer _pointsTimer; 

    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private void Awake()
    {

        _mainCamera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _startingPosition = gameObject.transform.position;
        _pointsTimer = FindObjectOfType<PointsTimer>();

    }

    private void OnEnable()
    {
        _mouseClick.Enable();
        _mouseClick.performed += MousePressed;
    }
    private void OnDisable()
    {
        _mouseClick.performed -= MousePressed;
        _rigidbody.useGravity = true;
        _mouseClick.Disable();
    }
    private void MousePressed(InputAction.CallbackContext context)
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
    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.CompareTag("Surface") || other.gameObject.CompareTag("Wall"))
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            gameObject.transform.position = _startingPosition;
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            if (_giveBaglocation != null)
            {
                Destroy(_giveBaglocation);
                _pointsTimer.OnOptionSelected(); 
                if (_isMonsterCandy)
                {
                    Debug.Log("The candy bag was destroyed by the monster candy.");
                }
                else
                {
                    Debug.Log("The candy bag was not destroyed by the monster candy.");
                }
            }
            else
            {
                Debug.Log("GiveBagLocation is already null");
            }
        }
    }
    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float _initialDistance = Vector3.Distance(clickedObject.transform.position, _mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
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
    private void OnMouseUp()
    {
        _rigidbody.useGravity = true;
    }
}
