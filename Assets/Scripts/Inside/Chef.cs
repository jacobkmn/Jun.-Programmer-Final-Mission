using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chef : MonoBehaviour
{
    [SerializeField] public ChefData chefData;

    [Header("Events")]
    [SerializeField] GameEvent OnOrderBeingPrepared;
    [SerializeField] GameEvent OnOrderReady;
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
    protected float endGameCurrent, endGameTarget;

    //When you eventually run into the problem of all chefs activating simultaneously, this is the solution
    //this is also referenced in chef reader
    bool isActive;
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public void Reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        current = 0;
        target = 0;
        endGameCurrent = 0;
        endGameTarget = 0;
        Destroy(GetComponent<AudioSource>());
    }

    //activates the respective character when door is clicked
    public virtual void ChefSequence()
    {
        target = target == 0 ? 1 : 0;
    }

    //the chef behind the clicked door moves to center stage
    protected IEnumerator MoveChef(float time)
    {
        if (DoorHandler.instance.DoorSelected == chefData.designatedDoor.ToString())
        {
            //Debug.Log("Door selected: " + DoorHandler.instance.DoorSelected);
            yield return new WaitForSeconds(1);

            float elapsedTime = 0;
            targetPosition = targetDestination.transform.position;

            if (target == 0) LookBack(); //if character is walking back to door, flip x

            //lerp inside of coroutine
            while (elapsedTime < time)
            {
                //Animate();
                current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(originalPosition, targetPosition, current);
                Animate();
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            //positions the chef and triggers dialogue in order of sequence
            //chef is saying hello for the first time
            if (target == 1 && UIMenuHandler.instance.OrderBeingPlaced == false)
            {
                ChefReader.instance.ChefNested = false;
                AudioManager.instance.ChangeSource("BOH", gameObject);
                FaceForward();
                //yield return new WaitForSeconds(0.3f);
                UIMenuHandler.instance.DisplayInitialDialogue();
            }
            //chef is returning to kitchen to prepare order
            else if (target == 0 && UIMenuHandler.instance.OrderBeingPlaced == true)
            {
                ChefReader.instance.ChefNested = true;
                ResetRotation();
                OnOrderBeingPrepared.Raise(); //tells the doors to close
                StartCoroutine(PrepareFood());
            }
            //chef is bringing order out
            else if (target == 1 && UIMenuHandler.instance.OrderBeingPlaced == true)
            {
                ChefReader.instance.ChefNested = false;
                FaceForward();
                //yield return new WaitForSeconds(0.3f);
                UIMenuHandler.instance.DisplayDeliveryDialogue();
            }
            //chef is back to square one
            else if (target == 0 && UIMenuHandler.instance.OrderBeingPlaced == false)
            {
                ChefReader.instance.ChefNested = true;
                ResetRotation();
                OnNestedChef.Raise(); //listened to by Doors
            }
        }
    }

    protected IEnumerator EndGameMoveChef(float time, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        endGameTarget = 1;

        //lerp inside of coroutine
        while (elapsedTime < time)
        {
            //Animate();
            endGameCurrent = Mathf.MoveTowards(endGameCurrent, endGameTarget, lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(originalPosition, targetPosition, endGameCurrent);
            Animate();
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.LookAt(Camera.main.transform.position);
    }

    //triggered when player steps on outside door trigger
    public void ItsAllOver()
    {
        if (GameManager.instance.GameOver)
            StopAllCoroutines();
    }

    //at this point, the user has clicked a food item on menu and chef has returned behind doors to prepare it
    protected IEnumerator PrepareFood()
    {
        yield return new WaitForSeconds(0.5f);
        AudioManager.instance.PlaySound("BOH");
        yield return new WaitForSeconds(15);

        OnOrderReady.Raise();
        //chef listens to this in inspector and responds with chef sequence
        //done in inspector because each chef subclass overrides the original chefsequence()

        yield return null;
    }

    //POLYMORPHISM - Handles different animation states for each chef
    protected abstract void Animate();

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
