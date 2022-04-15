using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    Transform outdoorCam;
    Vector3 originalPosition;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject doorTrigger;
    [SerializeField] ParticleSystem ps;
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
        while(!isFrozen)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                outdoorCam.position += Vector3.forward * moveSpeed * Time.deltaTime;
            }
            yield return null;
        }
        Debug.Log("Broke out of while loop");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == doorTrigger)
        {
            isFrozen = true;
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
        yield return new WaitForSeconds(2);

        float startTime = Time.time;
        while(Time.time < startTime + overTime)
        {
            ps.Play();
            outdoorCam.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);

            yield return null;
        }
        ps.Stop();
        indoorCam.gameObject.SetActive(true);
        gameObject.SetActive(false);
        gameObject.transform.position = originalPosition;
    }
}
