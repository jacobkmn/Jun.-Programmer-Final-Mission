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
        isTriggered = true;
        OnDoorTriggered.Raise();
        ParticleHandler.instance.SmokeStack.Stop();
        //Debug.Log("Doortrigger isTriggered = true");
    }
}
