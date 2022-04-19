using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] DoorData doorData;

    Animator anim;
    [SerializeField] float scaleMultiplier = 1.1f;
    Vector3 originalScale;

    //ENCAPSULATION
    public bool IsHovering { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsFrozen { get; private set; }

    int numberOfChildren;
    Material originalMaterial;
    [SerializeField] Material selectedMaterial;

    [Header("Event")]
    [SerializeField] GameEvent OnDoorClicked;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        originalScale = transform.localScale;
        numberOfChildren = transform.childCount;
        originalMaterial = transform.GetComponentInChildren<Renderer>().sharedMaterial;
    }

    void OnMouseOver()
    {
        if (!IsSelected && !IsFrozen)
        {
            transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
            IsHovering = true;
            HighlightDoor();
        }
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        IsHovering = false;
        RevertHighlightDoor();
    }

    void OnMouseDown()
    {
        if (!IsSelected && !IsFrozen)
        {
            DoorHandler.instance.DoorSelected = doorData.doorPosition.ToString();
            anim.SetBool("DoorTriggered", true);
            IsHovering = false;
            IsSelected = true;
            RevertHighlightDoor();
            OnDoorClicked.Raise();
        }
    }

    //reponse to a door being clicked. All doors listen to this
    public void DoorWasClicked()
    {
        IsFrozen = true;
    }

    void HighlightDoor()
    {
        if (IsHovering)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
                rend.sharedMaterial = selectedMaterial;
            }
        }
    }

    void RevertHighlightDoor()
    {
        if (!IsHovering)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
                rend.sharedMaterial = originalMaterial;
            }
        }
    }

    public void ChefIsPreparingFood()
    {
        if (IsSelected && IsFrozen)
        {
            StartCoroutine(DoorSwing());
        }
    }

    IEnumerator DoorSwing()
    {
        CloseDoor();

        yield return new WaitForSeconds(5);

        anim.SetBool("DoorTriggered", true);
    }

    public void CloseDoor()
    {
        if (IsSelected && IsFrozen)
        {
            anim.SetBool("DoorTriggered", false);
        }
    }

    //Response to OnFoodEaten event in Food class
    public void ResetDoors()
    {
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        WaitUntil chefNested = new WaitUntil(() => ChefReader.instance.ChefNested);
        yield return chefNested;

        yield return new WaitForSeconds(1.25f);

        IsFrozen = false;
        IsSelected = false;
    }
}