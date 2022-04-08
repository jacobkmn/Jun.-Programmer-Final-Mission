using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carla : Chef
{
    string[] dialogueOptions = new string[3];
    public override string DialogueOption {

        get { return dialogueOptions[Random.Range(0, 2)]; }

        set { dialogueOptions[Random.Range(0, 2)] = value; }

    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
        InitializeDialogue();
    }

    void InitializeDialogue()
    {
        dialogueOptions[0] = "What'll it be honey?";
        dialogueOptions[1] = "A fresh batch just came out the oven!";
        dialogueOptions[2] = "Try a cookie, mijo. The first one is on me.";
    }

    //private void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Space))
    //    //    target = target == 0 ? 1 : 0;

    //    //GoTo(targetPosition);

    //    //QueAnimation();

    //}

    public override void ChefSequence()
    {
        base.ChefSequence();
        StartCoroutine(MoveChef(2.15f));
        UIMenuHandler.instance.DialogueBarText.text = DialogueOption;
    }
}
