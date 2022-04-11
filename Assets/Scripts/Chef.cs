using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chef : MonoBehaviour
{
    [SerializeField] public ChefData chefData;

    [Header("Events")]
    [SerializeField] GameEvent OnNestedChef;

    protected Animator anim;

    [Header("Movement Info")]
    [SerializeField] GameObject focalPoint;
    [SerializeField] protected GameObject targetDestination;
    protected Vector3 originalPosition;
    protected Vector3 targetPosition;
    protected Quaternion originalRotation;
    [SerializeField] [Range(0f, 1.0f)] float lerpSpeed;
    protected float current, target;

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

        if (target == 0) LookBack(); //if character is walking back to door, flip x

        //lerp inside of coroutine
        while (elapsedTime < time)
        {
            Animate();
            current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, current);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //if character is coming out of doors, face forward and trigger dialogue
        //else if char just exited the scene, reset its rotation and trigger event that doors are listening to
        if (target == 1)
        {
            FaceForward();
            yield return new WaitForSeconds(0.3f);
            UIMenuHandler.instance.DisplayDialogue();
        }
        else if (target == 0)
        {
            ResetRotation();
            OnNestedChef.Raise();
        }
    }

    //ABSTRACTION - Handles animation states
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

    //ABSTRACTION - faces character forward once they've moved to center
    void FaceForward()
    {
            transform.LookAt(focalPoint.transform);
    }

    void ResetRotation()
    {
            transform.rotation = originalRotation;
    }

    void LookBack()
    {
        transform.LookAt(originalPosition);
    }
}
