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
    public void OnMenuLeft(InputValue value)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuLeft", value); 
    }

    public void OnMenuUp(InputValue value)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuUp", value);
    }

    public void OnStart(InputValue value)
    {
        CustomEvent.Trigger(this.gameObject, "OnStart", value);
    }

    public void OnMenuRight(InputValue value)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuRight", value);
    }

    public void OnMenuDown(InputValue value)
    {
        CustomEvent.Trigger(this.gameObject, "OnMenuDown", value);
    }
}
