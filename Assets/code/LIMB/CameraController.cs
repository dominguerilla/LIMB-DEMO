using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    public class CameraController : MonoBehaviour {

        private static CameraController _instance;
        public static CameraController Instance { get {return _instance; } }

        
        Camera mainCamera, currentCamera;
        bool cameraIsMoving;

        private void Awake() {
            if(_instance != null && _instance != this){
                Destroy(this);
            }else{
                _instance = this;
            }

            mainCamera = Camera.main;
            currentCamera = mainCamera;
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
    }
}
