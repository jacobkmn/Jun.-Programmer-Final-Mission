using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public static DoorHandler instance;

    private string doorSelected;
    public string DoorSelected
    {
        get { return doorSelected; }
        set { doorSelected = value; }
    }

    private void Awake()
    {
        instance = this;
    }
}
