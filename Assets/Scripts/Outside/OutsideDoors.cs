using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDoors : MonoBehaviour
{
    Animator anim;
    [SerializeField] float scaleMultiplier = 1.1f;
    Vector3 originalScale;
    int numberOfChildren;
    bool isHovering;
    bool isClicked;

    Material originalMaterial;
    [SerializeField] Material selectedMaterial;

    [Header("Event")]
    [SerializeField] GameEvent OnOutsideDoorClicked;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        originalScale = transform.localScale;
        numberOfChildren = transform.childCount;
        originalMaterial = transform.GetComponentInChildren<Renderer>().sharedMaterial;
    }

    void OnMouseOver()
    {
        if (DoorTrigger.instance.IsTriggered && !isClicked)
        {
            transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
            isHovering = true;
            HighlightDoor();
        }
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        isHovering = false;
        RevertHighlightDoor();
    }

    void OnMouseDown()
    {
        if (isHovering)
        {
            anim.SetBool("DoorTriggered", true);
            OnOutsideDoorClicked.Raise();
            RevertHighlightDoor();
            isClicked = true;
            OnOutsideDoorClicked.Raise();
        }
    }

    void HighlightDoor()
    {
        if (isHovering)
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
        if (!isHovering)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
                rend.sharedMaterial = originalMaterial;
            }
        }
    }
}
