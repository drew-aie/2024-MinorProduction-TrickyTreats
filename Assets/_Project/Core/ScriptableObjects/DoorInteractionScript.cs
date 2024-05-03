using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionScript : MonoBehaviour
{
    private float _Angle;
    [SerializeField]
    private float _target;
    private float _camDistance;
    [SerializeField]
    private Camera _mainCam;
    [SerializeField]
    private GameObject _gameObject;
    [SerializeField]
    private bool _isOpenable = true;
    [SerializeField]
    private Interactionscript[] _interactionScript;
    [SerializeField]
    private CameraShake _shake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {

        if (_isOpenable == true && Input.GetMouseButtonDown(0))
        {
            _shake.enabled = true;
            _shake.AddTrauma(.3f);
            _gameObject.transform.DORotate(new Vector3(0, -90, 0), 1, RotateMode.Fast);
            //_gameObject.transform.DOShakeScale(10, .5f, 10, 54, true, ShakeRandomnessMode.Harmonic);
            _isOpenable = false;
        }
        else if (_isOpenable == false && Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < _interactionScript.Length; i++)
            {
                if (_interactionScript[i].Moveable == false && Input.GetMouseButtonDown(0))
                {
                    _gameObject.transform.DORotate(new Vector3(0, -90, 0), 1, RotateMode.Fast);
                    _isOpenable = true;
                }
            }

        }
        _shake.enabled = false;

    }
}
