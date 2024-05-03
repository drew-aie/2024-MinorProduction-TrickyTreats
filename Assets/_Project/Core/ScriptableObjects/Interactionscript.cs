using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactionscript : MonoBehaviour
{
    private Renderer _renderer;
    private bool _isInteractive = false;
    private float _camDistance;
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private GameObject _giveBaglocation;
    private Vector3 _startingTime;
    private Vector3 _candyLocation;
    private Rigidbody _rigidbody;
    [SerializeField]
    private bool _isMoveable = true;
    public bool Moveable
    {
        get { return _isMoveable; }
        set { _isMoveable = value; }
    }
    [SerializeField]
    private bool _ismonsterCandy;
    public 
    // Start is called before the first frame update
    void Start()
    {
        Moveable = true;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _camDistance = _mainCam.WorldToScreenPoint(transform.position).z;
        _startingTime = transform.position;
        _candyLocation = _giveBaglocation.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "Surface" || other.collider.tag == "Wall")
        {
            _rigidbody.useGravity = false;
            _rigidbody.velocity = Vector3.zero;
            transform.position = _startingTime;
        }
        else if (other.collider.tag == "Finish")
        {
            Moveable = false;
            
            Destroy(_giveBaglocation);

        }
    }
    private void OnMouseDrag()
    {
        if (!_ismonsterCandy)
        {
            _renderer.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = Color.red;
        }

        _rigidbody.useGravity = false;
        Vector3 _screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camDistance);
        Vector3 _newPosition = _mainCam.ScreenToWorldPoint(_screenPosition);
        _rigidbody.MovePosition(_newPosition);

    }
   
    private void OnMouseUp()
    {
            _rigidbody.useGravity = true;
    }
}
