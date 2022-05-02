using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//swap rendering from current cam to targetcam
public class CameraSwap : MonoBehaviour
{
    [SerializeField] Camera targetCam;

    public void SwapCamera()
    {
        gameObject.SetActive(false);
        targetCam.gameObject.SetActive(true);
    }
}
