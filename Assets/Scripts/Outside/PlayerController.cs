using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform outdoorCam;
    Animator camAnim;
    Vector3 originalPosition;
    Quaternion originalRotation;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] Camera indoorCam;

    [Header("Game Events")]
    [SerializeField] GameEvent OnPlayerIsIndoors;
    [SerializeField] GameEvent OnPlayerIsRunning;
    [SerializeField] GameEvent OnPlayerHasFallen;
    [SerializeField] GameEvent OnEndGameFinished;

    bool isFrozen;

    bool GameStarted()
    {
        return GameManager.instance.GameStarted;
    }

    void Start()
    {
        outdoorCam = GetComponent<Transform>();
        camAnim = GetComponent<Animator>();
        camAnim.enabled = false;
        originalPosition = outdoorCam.transform.position;
        originalRotation = outdoorCam.transform.rotation;
        StartCoroutine(MovePlayer());
    }

    void OnEnable()
    {
        if (GameManager.instance.GameOver)
        {
            AudioManager.instance.PlaySound("EndGame");
        }
        else return;
    }

    IEnumerator MovePlayer()
    {
        while (!isFrozen)
        {
            if (Input.GetKey(KeyCode.UpArrow) && GameStarted())
            {
                outdoorCam.position += Vector3.forward * moveSpeed * Time.deltaTime;
                PlayerPosition.instance.IsMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && GameStarted())
            {
                AudioManager.instance.ChangeClip("Walking", Camera.main.gameObject);
                AudioManager.instance.PlaySound("Walking");
                //Debug.Log("Started Walking");
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow) && GameStarted())
            {
                AudioManager.instance.PauseSound("Walking");
                PlayerPosition.instance.IsMoving = false;
                //Debug.Log("Stopped Walking");
            }
            yield return null;
        }
        //Debug.Log("Broke out of while loop");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == doorTrigger && !GameManager.instance.GameOver)
        {
            isFrozen = true;
            AudioManager.instance.StopSound("Walking");
            AudioManager.instance.ChangeClip("EndGame", gameObject);
        }
    }

    //response to OnDoorClickedEvent that gets raised by OutsideDoors class
    public void OnDoorClicked()
    {
        isFrozen = false;
        StartCoroutine(DoorClickedSequence(outdoorCam.position, indoorCam.transform.position, 0.6f));
    }

    IEnumerator DoorClickedSequence(Vector3 source, Vector3 target, float overTime)
    {
        yield return new WaitForSeconds(4); //time it takes for door animation to complete (door opening)

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            ParticleHandler.instance.WindRush.Play(); //could alternatively fire an event here
            outdoorCam.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);

            yield return null;
        }
        ParticleHandler.instance.WindRush.Stop();
        ParticleHandler.instance.Rain.Stop();
        //indoorCam.gameObject.SetActive(true); //swap rendering camera
        //gameObject.SetActive(false);
       

        //yield return new WaitForSeconds(1);
        OnPlayerIsIndoors.Raise();
        indoorCam.gameObject.SetActive(true); //swap rendering camera
        gameObject.SetActive(false);
    }

    //triggered by OnLightDoneFlickering game event in inspector. Comes from LightBehavior Class
    public void EndGameSequence()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        camAnim.enabled = true;
        ParticleHandler.instance.Rain.Play();
        
        yield return new WaitForSeconds(5.0f); //time it takes for chefs to come center stage
        OnPlayerIsRunning.Raise(); //gets picked up by particlehandler to render the rain and smoke
        camAnim.SetTrigger("EndGame");
        yield return new WaitForSeconds(6.125f); //seconds until player "falls" in the animation
        OnPlayerHasFallen.Raise(); //triggers event for MenuUI to start blink_endgame animation

        //maybe reference audiomanager here to lerp fade the rain audio

        yield return new WaitForSeconds(7.0f);
        
        OnEndGameFinished.Raise(); //widespread message for many components to re-initialize for another playthrough
    }

    public void Reset()
    {
        camAnim.enabled = false;
        outdoorCam.transform.position = originalPosition;
        outdoorCam.transform.rotation = originalRotation;
        StartCoroutine(MovePlayer());
    }
}
