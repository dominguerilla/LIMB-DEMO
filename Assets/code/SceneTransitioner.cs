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
    Light battleLight, returnLight;

    private void Awake() {
        OnBattleStart = new UnityEvent();
        OnBattleEnd = new UnityEvent();
        BeginTransitionStarted = new UnityEvent();
        EndTransitionStarted = new UnityEvent();
    }

    private void Start()
    {
        InitializeImageFader();
        BattleManager bm = Locator.GetBattleManager();
        bm.onBattleStart.AddListener(CreateBattleScene);
        bm.onBattleEnd.AddListener(DestroyBattleScene);
    }

    public void CreateBattleScene()
    {
        (Combatant[], Combatant[]) parties = Locator.GetCombatants();
        CreateBattleScene(parties.Item1, parties.Item2, battleScenePrefab);
    }

    public void CreateBattleScene(Combatant[] leftParty, Combatant[] rightParty, GameObject battleScene, Camera returnCam = null, Light returnLight = null){
        StartCoroutine(InitializeBattleScene(leftParty, rightParty, battleScene, returnCam, returnLight));
    }

    void InitializeImageFader()
    {
        iFader = GetComponent<ImageFader>();
    }

    // TODO Make it so that it initializes a battle scene that already exists in the Scene.
    // That way, we don't have to instantiate a new one every battle
    IEnumerator InitializeBattleScene(Combatant[] party1, Combatant[] party2, GameObject battleScene, Camera returnCam = null, Light returnLight = null){
        yield return StartCoroutine(iFader.BattleStartFadeOut());
        GameObject newScene = Instantiate(battleScene, scenePosition.position, scenePosition.rotation) as GameObject;
        activeBattleScene = newScene;
            
        // Initialize camera
        Camera battleCam = newScene.GetComponentInChildren<Camera>();
        CameraController.Instance.SetActiveCamera(battleCam);

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

        SpawnParties(newScene, party1, party2);

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
            
        CameraController.Instance.ResetActiveCamera();
        Destroy(activeBattleScene);
        activeBattleScene = null;
        EndTransitionStarted.RemoveAllListeners();
        yield return StartCoroutine(iFader.BattleEndFadeIn());        
    }

    void SpawnParties(GameObject instantiatedScene, Combatant[] party1, Combatant[] party2)
    {
        GameObject party1Parent = instantiatedScene.transform.Find("Party1Positions").gameObject;
        GameObject party2Parent = instantiatedScene.transform.Find("Party2Positions").gameObject;
        Transform[] party1Positions = GetPositions(party1Parent);
        SpawnParty(party1, party1Positions, party1Parent.transform);

        Transform[] party2Positions = GetPositions(party2Parent);
        SpawnParty(party2, party2Positions, party2Parent.transform);
    }
    Transform[] GetPositions(GameObject parentObj){
        List<Transform> positions = new List<Transform>();
        foreach (Transform t in parentObj.transform){
            if(t.gameObject.name.StartsWith("Position")){
                positions.Add(t);
            }
        }
        return positions.ToArray();
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
            BeginTransitionStarted.AddListener(delegate { combatant.PlayAnimation("OnSpawn"); });
            EndTransitionStarted.AddListener(delegate { combatant.PlayAnimation("OnDeath"); });
        }
    }
}
