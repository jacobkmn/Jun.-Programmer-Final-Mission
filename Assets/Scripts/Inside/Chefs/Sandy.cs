using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandy : Chef
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
        StartCoroutine(MoveChef(2.15f)); //moves chef to center stage
    }

    protected override void Animate()
    {
        if (transform.position == originalPosition)
        {
            anim.SetBool("Static_b", true);
            anim.SetFloat("Speed_f", 0);
            //anim.SetInteger("Animation_int", 0);
            StopCoroutine(Smoking());
        }
        else if (transform.position == targetPosition)
        {
            anim.SetBool("Static_b", true);
            anim.SetFloat("Speed_f", 0);
            StartCoroutine(Smoking());
        }
        else
        {
            StopCoroutine(Smoking());
            anim.SetBool("Static_b", false);
            anim.SetFloat("Speed_f", 0.3f);
            //anim.SetInteger("Animation_int", 0);
        }
    }

    //alternates between idle state, and taking a puff at random intervals
    IEnumerator Smoking()
    {
        while (transform.position == targetPosition)
        {
            anim.SetBool("ToIdle", false);
            anim.SetTrigger("smoke");
            yield return new WaitForSeconds(3);
            anim.SetBool("ToIdle", true);
            yield return new WaitForSeconds(Random.Range(4, 10));
        }
    }
}
