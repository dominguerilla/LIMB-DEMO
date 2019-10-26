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
	    camControl = CameraController.Instance;
	}

    public void ZoomIn(){
        if(!isZoomed && target){
            originalPosition = CameraController.Instance.GetActiveCamera().transform.position;
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
