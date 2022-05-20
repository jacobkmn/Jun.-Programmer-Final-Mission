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
    public bool IsHovering
    {
        get { return isHovering; }
    }
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

    public void Reset()
    {
        AudioManager.instance.ChangeClip("OutsideDoor_open", gameObject);
    }

    public void CloseOutsideDoors()
    {
        isClicked = false;
        anim.SetBool("OutsideDoorTriggered", false);
    }

    public void SlamDoor()
    {
        AudioManager.instance.ChangeClip("OutsideDoor_close", gameObject);
        AudioManager.instance.PlaySoundDelayed("OutsideDoor_close", 0.5f); //was 0.02f
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
            RevertHighlightDoor();
            isClicked = true;
            OnOutsideDoorClicked.Raise(); //starts DoorSequence coroutine in playercontroller, attached to OutsideCamera
            anim.SetBool("OutsideDoorTriggered", true);
            AudioManager.instance.PlaySoundDelayed("OutsideDoor_open", 0.5f);
            AudioManager.instance.PlaySoundDelayed("Ghost", 1.5f);
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
