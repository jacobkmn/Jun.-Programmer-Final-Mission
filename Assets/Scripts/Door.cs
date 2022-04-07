using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    [SerializeField] float scaleMultiplier = 1.1f;
    Vector3 originalScale;

    //encapsulation
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
            for (int i = 0; i < numberOfChildren; i++)
            {
                Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
                rend.sharedMaterial = selectedMaterial;
            }
        }
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        IsHovering = false;
        for (int i = 0; i < numberOfChildren; i++)
        {
            Renderer rend = transform.GetChild(i).gameObject.GetComponent<Renderer>();
            rend.sharedMaterial = originalMaterial;
        }
    }

    void OnMouseDown()
    {
        if (!IsSelected && !IsFrozen)
        {
            anim.SetBool("DoorTriggered", true);
            IsSelected = true;
            OnDoorClicked.Raise();
        }
    }

    //reponse to a door being clicked. All doors listen to this
    public void DoorWasClicked()
    {
            IsFrozen = true;
    }
}
