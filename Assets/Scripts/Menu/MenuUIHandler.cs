using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Animator eyes;

    void Blink()
    {
        eyes.SetBool("Blink", true);
    }
}
