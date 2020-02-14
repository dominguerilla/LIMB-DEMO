using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    public GameObject model;
    public string animationToTrigger;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = model.GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("No Animator component found in " + model.gameObject);
            Destroy(this);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.Play(animationToTrigger);
        }
    }
}
