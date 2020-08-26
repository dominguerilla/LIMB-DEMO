using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class CameraController : MonoBehaviour {
        
    Camera mainCamera, currentCamera;
    bool cameraIsMoving;

    private void Awake() {
        mainCamera = Camera.main;
        currentCamera = mainCamera;
        Locator.Provide(this);
    }
        
    /// <summary>
    /// Sets the active camera to the specified one. 
    /// </summary>
    public void SetActiveCamera(Camera cam){
        if(this.currentCamera){
            this.currentCamera.enabled = false;
        }
        this.currentCamera = cam;
        this.currentCamera.enabled = true;
    }

    /// <summary>
    /// Disables the currentCamera and re-enables the main camera.
    /// </summary>
    public void ResetActiveCamera(){
        if(this.currentCamera){
            this.currentCamera.enabled = false;
        }
        this.currentCamera = mainCamera;
        this.currentCamera.enabled = true;
    }

    public void MoveCamera(Vector3 newLocation, float camSpeed = 5.0f){
        if(!cameraIsMoving){
            StartCoroutine(LerpCamera(newLocation, camSpeed));
        }
    }

    public Camera GetActiveCamera(){
        return this.currentCamera;
    }

    IEnumerator LerpCamera(Vector3 newLocation, float camSpeed){
        cameraIsMoving = true;
        while(currentCamera.transform.position != newLocation){
            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, newLocation, Time.deltaTime * camSpeed);
            yield return null;
        }
        cameraIsMoving = false;
    }

    public void Test()
    {
        StartRotateCameraAroundPoint(new Vector3(7, 7, -15), 5.0f);
    }

    public void StartRotateCameraAroundPoint(Vector3 targetLocation, float camSpeed)
    {
        StartCoroutine(RotateCameraAroundPoint(targetLocation, camSpeed));
    }

    IEnumerator RotateCameraAroundPoint(Vector3 targetLocation, float camSpeed)
    {
        while (true)
        {
            currentCamera.transform.RotateAround(targetLocation, Vector3.up, Time.deltaTime * camSpeed);
            currentCamera.transform.LookAt(targetLocation);
            yield return null;
        }
    }
}

