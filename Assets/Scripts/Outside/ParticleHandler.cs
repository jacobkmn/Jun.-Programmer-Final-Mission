using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public static ParticleHandler instance;

    private void Awake()
    {
        instance = this;
        smokeStack.Play();
    }

    [SerializeField] ParticleSystem windRush;
    public ParticleSystem WindRush
    {
        get { return windRush; }
    }
    [SerializeField] ParticleSystem smokeStack;
    public ParticleSystem SmokeStack
    {
        get { return smokeStack; }
    }

}
