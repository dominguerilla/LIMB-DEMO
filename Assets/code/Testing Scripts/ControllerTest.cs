using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour
{
    BattleTester bt;
    bool inBattle = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Controller test script present!");
        bt = FindObjectOfType<BattleTester>();
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
