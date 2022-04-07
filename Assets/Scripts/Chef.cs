using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chef : MonoBehaviour
{
    public virtual void GoTo(Vector3 position)
    {

    }

    public abstract void PrintDialogue();

}
