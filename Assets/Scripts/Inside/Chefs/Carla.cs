using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carla : Chef
{
    private void Start()
    {
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    //POLYMORPHISM - each chef moves at dif pace and has dif dialogue options
    public override void ChefSequence()
    {
            base.ChefSequence();
            StartCoroutine(MoveChef(2.69f)); //moves chef to center stage
    }

    public void EndGameChefSequence()
    {
        Vector3 offset = new Vector3(1.2f, 0,0);
        targetPosition = targetDestination.transform.position + offset;
        StartCoroutine(EndGameMoveChef(2.69f, targetPosition)); //moves chef to stage center-right
    }

    protected override void Animate()
    {
        if (transform.position == originalPosition)
        {
            anim.SetBool("Static_b", true);
            anim.SetFloat("Speed_f", 0);
            anim.SetInteger("Animation_int", 0);
        }
        else if (transform.position == targetPosition)
        {
            anim.SetBool("Static_b", true);
            anim.SetFloat("Speed_f", 0);
            anim.SetInteger("Animation_int", 2);
        }
        else
        {
            anim.SetBool("Static_b", false);
            anim.SetFloat("Speed_f", 0.3f);
            anim.SetInteger("Animation_int", 0);
        }
    }
}
