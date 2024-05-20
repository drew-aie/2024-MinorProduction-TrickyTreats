using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public static class EventManager
{
    public static event UnityAction<InputAction.CallbackContext> MouseHeld;
    public static void _onMouseHeld(InputAction.CallbackContext context) => MouseHeld?.Invoke(context);
}
