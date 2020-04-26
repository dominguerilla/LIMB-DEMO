using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantAnimator : MonoBehaviour
{
    Vector3 startingPosition;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = this.transform.position;
        anim = this.GetComponent<Animator>();
        if (!anim)
        {
            anim = GetComponentInChildren<Animator>();
        }
    }

    public void TriggerAnimation(string animationName)
    {
        anim.SetTrigger(animationName);
    }
}
