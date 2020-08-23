using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LIMB;

public class CameraTester : MonoBehaviour {

    [SerializeField]
    public GameObject target;
    [SerializeField]
    public Vector3 distance;

    CameraController camControl;
    Vector3 originalPosition;
    bool isZoomed;

	// Use this for initialization
	void Start () {
        camControl = Locator.GetCameraController();
	}

    public void ZoomIn(){
        if(!isZoomed && target){
            originalPosition = camControl.GetActiveCamera().transform.position;
            camControl.MoveCamera(target.transform.position + distance);
            isZoomed = true;
        }
    }

    public void ZoomOut(){
        if(isZoomed){
            camControl.MoveCamera(originalPosition);
            originalPosition = Vector3.zero;
            isZoomed = false;
        }
    }
}
