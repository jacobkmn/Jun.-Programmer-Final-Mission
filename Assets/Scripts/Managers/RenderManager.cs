using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    [SerializeField] GameObject[] NotRenderedOnAwake;
    [SerializeField] GameObject[] NotRenderedOnEyesOpen;
    [SerializeField] GameObject[] RenderedOnEyesOpen;

    private void Start()
    {
        foreach (GameObject item in NotRenderedOnAwake)
        {
            item.SetActive(false);
        }
    }

    public void TurnOffObjects()
    {
        foreach  (GameObject item in NotRenderedOnEyesOpen)
        {
            item.SetActive(false);
        }
    }

    public void RenderObjects()
    {
        foreach (GameObject item in RenderedOnEyesOpen)
        {
            item.SetActive(true);
        }
    }
}
