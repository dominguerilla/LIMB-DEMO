using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using LIMB;

/// <summary>
/// Executes the transition between the field and a battle.
/// Initializes battle 'stage', instantiates the combatant models, destroys stage after battle ends.
/// </summary>
[RequireComponent(typeof(ImageFader))]
public class SceneTransitioner : MonoBehaviour {

    public GameObject battleScenePrefab;
    public Transform scenePosition;

    ImageFader iFader;
        
    /// <summary>
    /// Called whenever a battle starts.
    /// </summary>
    [HideInInspector]
    public UnityEvent OnBattleStart;

    /// <summary>
    /// Called whenever a battle ends.
    /// </summary>
    [HideInInspector]
    public UnityEvent OnBattleEnd;

    /// <summary>
    /// Called when the scene STARTS to fade in FROM black, during the transition from field to battle.
    /// All listeners are cleared after a battle has ended.
    /// </summary>
    [HideInInspector]
    public UnityEvent BeginTransitionStarted;

    /// <summary>
    /// Called when the scene STARTS to fade TO black, during the transition from battle to field.
    /// All listeners are cleared after a battle has ended.
    /// </summary>
    [HideInInspector]
    public UnityEvent EndTransitionStarted;

    GameObject activeBattleScene;
    BattleScene battleSceneInfo;
    Light battleLight, returnLight;

    private void Awake() {
        OnBattleStart = new UnityEvent();
        OnBattleEnd = new UnityEvent();
        BeginTransitionStarted = new UnityEvent();
        EndTransitionStarted = new UnityEvent();
    }

    private void Start()
    {
        iFader = GetComponent<ImageFader>();
        BattleManager bm = Locator.GetBattleManager();
        GetBattleScene();
        bm.onBattleStart.AddListener(CreateBattleScene);
        bm.onBattleEnd.AddListener(DestroyBattleScene);
    }

    void GetBattleScene()
    {
        battleSceneInfo = battleScenePrefab.GetComponent<BattleScene>();
        if (!battleSceneInfo)
        {
            Debug.LogError("No Battle Scene set on battleScenePrefab!");
            Destroy(this);
        }
    }

    public void CreateBattleScene()
    {
        (Combatant[], Combatant[]) parties = Locator.GetCombatants();
        CreateBattleScene(parties.Item1, parties.Item2, battleScenePrefab);
    }

    public void CreateBattleScene(Combatant[] leftParty, Combatant[] rightParty, GameObject battleScene, Camera returnCam = null, Light returnLight = null){
        StartCoroutine(InitializeBattleScene(leftParty, rightParty, battleScene, returnCam, returnLight));
    }

    // TODO Make it so that it initializes a battle scene that already exists in the Scene.
    // That way, we don't have to instantiate a new one every battle
    IEnumerator InitializeBattleScene(Combatant[] party1, Combatant[] party2, GameObject battleScene, Camera returnCam = null, Light returnLight = null){
        yield return StartCoroutine(iFader.BattleStartFadeOut());
        GetBattleScene();
        GameObject newScene = Instantiate(battleScene, scenePosition.position, scenePosition.rotation) as GameObject;
        activeBattleScene = newScene;
            
        // Initialize camera
        Camera battleCam = newScene.GetComponentInChildren<Camera>();
        CameraController camControl = Locator.GetCameraController();
        camControl.SetActiveCamera(battleCam);

        // Initialize lights
        // Assumes there's only one light in battleScene, and one return light
        this.battleLight = newScene.GetComponentInChildren<Light>();
        if(this.battleLight){
            this.battleLight.enabled = true;
        }
        this.returnLight = returnLight;
        if(this.returnLight){
            this.returnLight.enabled = false;
        }

        SpawnParties(battleSceneInfo, party1, party2);
        BeginTransitionStarted.Invoke();
        yield return StartCoroutine(iFader.BattleStartFadeIn());
        OnBattleStart.Invoke();
    }

    public void DestroyBattleScene(){
        StartCoroutine(TakeDownBattleScene());
    }

    IEnumerator TakeDownBattleScene(){
        OnBattleEnd.Invoke();
        EndTransitionStarted.Invoke();

        BeginTransitionStarted.RemoveAllListeners();

        yield return StartCoroutine(iFader.BattleEndFadeOut());
        if(returnLight){
            returnLight.enabled = true;
            returnLight = null;
        }

        if(battleLight){
            battleLight.enabled = false;
            battleLight = null;
        }

        CameraController camControl = Locator.GetCameraController();
        camControl.ResetActiveCamera();
        Destroy(activeBattleScene);
        activeBattleScene = null;
        EndTransitionStarted.RemoveAllListeners();
        yield return StartCoroutine(iFader.BattleEndFadeIn());        
    }

    void SpawnParties(BattleScene instantiatedScene, Combatant[] party1, Combatant[] party2)
    {
        (Transform[], Transform[]) partyPositions = instantiatedScene.GetPartyPositions();
        SpawnParty(party1, partyPositions.Item1);
        SpawnParty(party2, partyPositions.Item2);
    }


    void SpawnParty(Combatant[] party, Transform[] positions, Transform parent = null){
        for(int i = 0; i < party.Length; i++){
            if(i >= positions.Length){
                break;
            }
            Combatant combatant = party[i];
            GameObject newObj = GameObject.Instantiate<GameObject>(
                combatant.GetData().GetModel(), 
                positions[i].transform.position,
                positions[i].transform.rotation,
                parent );
            combatant.SetGameObject(newObj);
            combatant.InitializeCombatantComponents();
        }
    }
}
