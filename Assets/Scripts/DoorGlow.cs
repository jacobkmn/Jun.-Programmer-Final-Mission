using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGlow : MonoBehaviour
{
    Material[] materials;
    Renderer rend;

    private void Start()
    {
        materials = rend.GetComponent<MeshRenderer>().sharedMaterials;
    }

    public void SelectDoor()
    {

    }

    public void DeSelectDoor()
    {

    }
}
