using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chef : MonoBehaviour
{
    protected Animator anim;

    [SerializeField] GameObject focalPoint;
    [SerializeField] protected GameObject targetDestination;
    protected Vector3 originalPosition;
    protected Vector3 targetPosition;
    [SerializeField] [Range(0f, 1.0f)] float lerpSpeed;
    protected float current, target;

    //each inidivudal chef class has their own dialogue options
    public abstract string DialogueOption { get; set; }

    //activates the respective character when door is clicked
    public virtual void ChefSequence()
    {
        target = target == 0 ? 1 : 0;
    }

    //chef moves to center stage
    protected IEnumerator MoveChef(float time)
    {
        yield return new WaitForSeconds(1);

        float elapsedTime = 0;

        targetPosition = targetDestination.transform.position;

        while (elapsedTime < time)
        {
            Animate();
            current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, current);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        FaceForward();
        yield return new WaitForSeconds(0.3f);
        UIMenuHandler.instance.DisplayDialogue();
        //transform.position = targetPosition;
    }

    //Handles animation states
    protected void Animate()
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

    void FaceForward()
    {
        transform.LookAt(focalPoint.transform);
    }
}
