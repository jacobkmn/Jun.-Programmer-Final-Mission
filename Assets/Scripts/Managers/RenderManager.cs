using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    [SerializeField] GameObject[] NotRenderedOnAwake;
    [SerializeField] GameObject[] NotRenderedOnEyesOpen;
    [SerializeField] GameObject[] RenderedOnEyesOpen;
    [SerializeField] GameObject[] NotRenderedWhenPlayerIsIndoors;

    private void Start()
    {
        InitialRender();
    }

    public void InitialRender()
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

    public void TurnOffObjectsWhenIndoors()
    {
        foreach (GameObject item in NotRenderedWhenPlayerIsIndoors)
        {
            item.SetActive(false);
        }
    }

}
