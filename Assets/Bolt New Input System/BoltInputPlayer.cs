using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoltInputPlayer : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;

    private List<InputAction> inputActions = new List<InputAction>();

    void Awake()
    {
        inputActionAsset.Enable();
        foreach (InputAction inputAction in inputActionAsset)
        {
            inputAction.performed += OnActionPerformed;
            inputAction.started += OnActionStarted;
            inputAction.canceled += OnActionCanceled;
            inputAction.Enable();
            inputActions.Add(inputAction);
        }
    }

    private void OnDisable()
    {
        foreach (InputAction inputAction in inputActions)
        {
            inputAction.Disable();
        }
    }

    private void OnActionPerformed(InputAction.CallbackContext callbackContext)
    {
        CustomEvent.Trigger(gameObject, "OnActionPerformed", callbackContext);
    }

    private void OnActionStarted(InputAction.CallbackContext callbackContext)
    {
        CustomEvent.Trigger(gameObject, "OnActionStarted", callbackContext);
    }

    private void OnActionCanceled(InputAction.CallbackContext callbackContext)
    {
        CustomEvent.Trigger(gameObject, "OnActionCanceled", callbackContext);
    }
}