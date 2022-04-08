using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carla : Chef
{
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    target = target == 0 ? 1 : 0;

        //GoTo(targetPosition);

        //QueAnimation();
        
    }

    public override void PrintDialogue()
    {
        //throw new System.NotImplementedException();
    }
}
