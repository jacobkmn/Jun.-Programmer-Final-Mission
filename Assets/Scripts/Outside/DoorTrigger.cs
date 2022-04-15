using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public static DoorTrigger instance;

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
        isTriggered = true;
        //Debug.Log("Doortrigger isTriggered = true");
    }
}
