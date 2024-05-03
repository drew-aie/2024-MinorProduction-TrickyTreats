using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementBehaviourScript : MonoBehaviour
{
    private Vector3 _movementInput;
    // Start is called before the first frame update
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.action.ReadValue<Vector3>();


    }
    private void Update()
    {
        Debug.Log(_movementInput);
    }

}
