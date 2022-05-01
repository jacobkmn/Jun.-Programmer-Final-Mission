using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] DoorData doorData;
    [SerializeField] ParticleSystem doorSmoke;

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
        if (!IsSelected && !IsFrozen && !GameIsOver())
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
        if (!IsSelected && !IsFrozen && !GameIsOver())
        {
            AudioManager.instance.ChangeSource("InsideDoors_open", gameObject);
            //AudioManager.instance.ChangeSource("InsideDoors_close", gameObject);
            DoorHandler.instance.DoorSelected = doorData.doorPosition.ToString();
            IsHovering = false;
            IsSelected = true;
            OpenDoor();
            RevertHighlightDoor();
            OnDoorClicked.Raise();
        }
    }

    //reponse to a door being clicked. All doors listen to this
    public void DoorWasClicked()
    {
        IsFrozen = true;
    }

    //highlight the children (both doors)
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

        yield return new WaitForSeconds(15);

        OpenDoor();
    }

    public void CloseDoor()
    {
        if (IsSelected && IsFrozen)
        {
            anim.SetBool("DoorTriggered", false);
            AudioManager.instance.ChangeClip("InsideDoors_close", gameObject);
            AudioManager.instance.PlaySound("InsideDoors_close");
        }
    }
    public void OpenDoor()
    {
        if (GameManager.instance.GameOver)
        {
            anim.SetBool("DoorTriggered", true);
            //start particle system
        }
        else
        {
            anim.SetBool("DoorTriggered", true);
            AudioManager.instance.ChangeClip("InsideDoors_open", gameObject);
            AudioManager.instance.PlaySoundDelayed("InsideDoors_open", 0.5f);
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

    bool GameIsOver()
    {
        return GameManager.instance.GameOver;
    }
}