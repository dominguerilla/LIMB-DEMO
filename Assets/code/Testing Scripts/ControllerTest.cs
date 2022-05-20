using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;

public class ControllerTest : MonoBehaviour
{
    BattleTester bt;
    GameObject bmObject;
    bool inBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Controller test script present!");
        bt = FindObjectOfType<BattleTester>();
        bmObject = FindObjectOfType<BattleManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (inBattle)
        {
            BattleInput();
        }
        else
        {
            OutOfBattleInput();
        }
    }

    void BattleInput()
    {
        if (Input.GetKeyUp(KeyCode.Joystick1Button3))
        {
            Debug.Log("ATK MENU");
            CustomEvent.Trigger(bmObject, "Action Type Selected");
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            Debug.Log("RUN AWAY MENU");
            CustomEvent.Trigger(bmObject, "Action Type Selected");
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            Debug.Log("MAG MENU");
            CustomEvent.Trigger(bmObject, "Action Type Selected");
        }
        else if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            Debug.Log("ITEM MENU");
            CustomEvent.Trigger(bmObject, "Action Type Selected");
        }
    }

    void OutOfBattleInput()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            bt.StartBattle();
        }
    }

    public void EnableBattleInput()
    {
        inBattle = true;
    }

    public void DisableBattleInput()
    {
        inBattle = false;
    }
}
