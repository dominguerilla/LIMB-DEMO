using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    /// <summary>
    /// The position of the center of the stage.
    /// </summary>
    [SerializeField]
    private Transform centerStageLocation;

    /// <summary>
    /// The Transform that holds the spawn positions of combatants of party 1 as child objects.
    /// </summary>
    public Transform party1Positions;

    /// <summary>
    /// The Transform that holds the spawn positions of combatants of party 2 as child objects.
    /// </summary>
    public Transform party2Positions;

    /// <summary>
    /// The prefix of Transforms that signify the spawn positions of combatants.
    /// </summary>
    public string characterPositionPrefix = "Position";

    private void Awake()
    {
        if (!party1Positions || !party2Positions)
        {
            Debug.LogError("Party positions not set!");
        }

        if (!centerStageLocation)
        {
            Debug.LogError("Center stage position not set!");
        }
    }

    /// <summary>
    /// Finds the positions of party 1 and party 2.
    /// </summary>
    /// <returns>(Party 1 locations, Party 2 locations) </returns>
    public (Transform[], Transform[]) GetPartyPositions()
    {
        return (GetCombatantPositions(party1Positions), GetCombatantPositions(party2Positions));
    }

    Transform[] GetCombatantPositions(Transform partyPositionObject)
    {
        List<Transform> positions = new List<Transform>();
        foreach (Transform t in partyPositionObject.transform)
        {
            if (t.gameObject.name.StartsWith(characterPositionPrefix))
            {
                positions.Add(t);
            }
        }
        return positions.ToArray();
    }

    public Vector3 GetCenterStagePosition()
    {
        return centerStageLocation.position;
    }
}
