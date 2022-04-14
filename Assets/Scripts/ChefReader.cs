using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefReader : MonoBehaviour
{
    public static ChefReader instance { get; private set; }
    public Chef currentChef { get; private set; }

    bool chefNested;
    public bool ChefNested
    {
        get { return chefNested; }
        set { chefNested = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        currentChef = other.gameObject.GetComponent<Chef>();
        currentChef.IsActive = true;
        print("Current chef is: " + currentChef);
    }

    private void OnTriggerExit(Collider other)
    {
        currentChef = null;
    }

}
