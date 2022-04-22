using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Transform outdoorCam;
    Vector3 originalPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] Camera indoorCam;

    bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        outdoorCam = GetComponent<Transform>();
        originalPosition = outdoorCam.transform.position;
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        while (!isFrozen)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                outdoorCam.position += Vector3.forward * moveSpeed * Time.deltaTime;
                PlayerPosition.instance.IsMoving = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                AudioManager.instance.PlaySound("Walking");
                //Debug.Log("Started Walking");
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
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
        if (other.gameObject == doorTrigger)
        {
            isFrozen = true;
            AudioManager.instance.StopSound("Walking");
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
        AudioManager.instance.PlaySound("Outside_door");
        AudioManager.instance.PlaySoundDelayed("Ghost", 1.5f);
        yield return new WaitForSeconds(4); //time it takes for door animation to complete (door opening)

        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            ParticleHandler.instance.WindRush.Play(); //could alternatively fire an event here
            outdoorCam.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);

            yield return null;
        }
        ParticleHandler.instance.WindRush.Stop();
        ParticleHandler.instance.SmokeStack.Stop();
        ParticleHandler.instance.Rain.Stop();
        indoorCam.gameObject.SetActive(true);
        gameObject.SetActive(false);
        gameObject.transform.position = originalPosition;
    }
}
