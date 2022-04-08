using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chef : MonoBehaviour
{
    protected Animator anim;

    [SerializeField] protected GameObject targetDestination;
    protected Vector3 originalPosition;
    protected Vector3 targetPosition;
    [SerializeField] [Range(0f, 1.0f)] float lerpSpeed;
    protected float current, target;



    //each inidivudal chef class has their own dialogue options
    public abstract void PrintDialogue();

    //activates the respective character when door is clicked
    protected void ChefSequence()
    {
        target = target == 0 ? 1 : 0;
        StartCoroutine(MoveChef());
        //GoTo(targetPosition);
    }

    //chef moves to center stage
    IEnumerator MoveChef()
    {
        yield return new WaitForSeconds(1);

        float elapsedTime = 0;
        float time = 4;
        originalPosition = transform.position;
        targetPosition = targetDestination.transform.position;

        while (elapsedTime < time)
        {
            QueAnimation();
            current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, current);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    //protected void GoTo(Vector3 position)
    //{
    //    QueAnimation();

    //    current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
    //    transform.position = Vector3.Lerp(originalPosition, targetPosition, current);
    //}

    //Handles animation states
    protected void QueAnimation()
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
