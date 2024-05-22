//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Project/Characters/Player/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Mouse"",
            ""id"": ""73f4e933-5aec-496d-9444-6186b965f1cc"",
            ""actions"": [
                {
                    ""name"": ""mouseClick"",
                    ""type"": ""Value"",
                    ""id"": ""ab8e2fa2-8484-4e95-a696-ac11f2a77ccd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8439ca4e-b87b-4771-8f45-9d2bb650c8d5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GameControl"",
                    ""action"": ""mouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc4162f4-6296-4f82-b15d-80fed7fc2740"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""GameControl"",
            ""bindingGroup"": ""GameControl"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_mouseClick = m_Mouse.FindAction("mouseClick", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_mouseClick;
    public struct MouseActions
    {
        private @Controls m_Wrapper;
        public MouseActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @mouseClick => m_Wrapper.m_Mouse_mouseClick;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @mouseClick.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
                @mouseClick.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
                @mouseClick.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnMouseClick;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @mouseClick.started += instance.OnMouseClick;
                @mouseClick.performed += instance.OnMouseClick;
                @mouseClick.canceled += instance.OnMouseClick;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    private int m_GameControlSchemeIndex = -1;
    public InputControlScheme GameControlScheme
    {
        get
        {
            if (m_GameControlSchemeIndex == -1) m_GameControlSchemeIndex = asset.FindControlSchemeIndex("GameControl");
            return asset.controlSchemes[m_GameControlSchemeIndex];
        }
    }
    public interface IMouseActions
    {
        void OnMouseClick(InputAction.CallbackContext context);
    }
}
