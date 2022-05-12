using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public static DoorTrigger instance;

    [SerializeField] GameEvent OnDoorTriggered;

    bool isTriggered;
    public bool IsTriggered
    {
        get { return isTriggered; }
    }

    void Awake()
    {
        instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.GameOver)
        {
            isTriggered = true;
            OnDoorTriggered.Raise();
            //Debug.Log("Doortrigger isTriggered = true");
        }
    }

    void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
}
