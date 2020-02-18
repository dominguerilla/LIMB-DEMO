using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Debug.Log("ATK MENU");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            Debug.Log("RUN AWAY MENU");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Debug.Log("MAG MENU");
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Debug.Log("ITEM MENU");
        }
    }
}
