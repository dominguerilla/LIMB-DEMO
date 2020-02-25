using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/// <summary>
/// Makes Input from the PlayerInput object on this GameObject trigger Bolt Custom Events.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputToBolt : MonoBehaviour
{
    public void OnMenuLeft(InputAction.CallbackContext context)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuLeft", context); 
    }

    public void OnMenuUp(InputAction.CallbackContext context)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuUp", context);
    }

    public void OnStart(InputAction.CallbackContext context)
    {
        CustomEvent.Trigger(this.gameObject, "OnStart", context);
    }
}
