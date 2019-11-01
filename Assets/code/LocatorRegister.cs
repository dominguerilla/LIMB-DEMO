using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatorRegister : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Awake()
    {
        BattleManager bm = GameObject.FindObjectOfType<BattleManager>();
        if(bm != null){
            Locator.Provide(bm);
        }else{
            Debug.LogError("No BattleManager found in scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
