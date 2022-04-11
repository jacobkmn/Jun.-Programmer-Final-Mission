using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carla : Chef
{
    private void Start()
    {
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    //POLYMORPHISM - each chef moves at dif pace and has dif dialogue options
    public override void ChefSequence()
    {
        base.ChefSequence();
        StartCoroutine(MoveChef(2.15f)); //moves chef to center stage
    }
}
