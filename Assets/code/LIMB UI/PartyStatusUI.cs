using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using UnityEngine.UI;

public class PartyStatusUI : MonoBehaviour
{
    private static int MAX_PARTY_SIZE = 4;
    Combatant[] party;

    [SerializeField]
    Canvas statusCanvas;

    [SerializeField]
    GameObject[] statusObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        if(statusObjects == null ||statusObjects.Length < MAX_PARTY_SIZE){
            Debug.LogError("Not enough status objects!");
            Destroy(this);
        }
    }

    public void CreatePartyStatus(){
        party = Locator.GetCombatants().Item1;
        statusCanvas.enabled = true;
        UpdateStatus();
    }

    public void UpdateStatus(){
        int i = 0;
        foreach(Combatant combatant in party){
            Text text = statusObjects[i].GetComponentInChildren<Text>();
            text.text = CreateStatus(combatant);
            i++;
        }
    }

    public void DisableStatus(){
        statusCanvas.enabled = false;
        int i = 0;
        foreach(Combatant combatant in party){
            Text text = statusObjects[i].GetComponentInChildren<Text>();
            text.text = "";
            i++;
        }

    }

    string CreateStatus(Combatant combatant){
        return string.Format("{0}\nHP: {1}", combatant.GetName(), combatant.GetCurrentHealth());
    }

}
