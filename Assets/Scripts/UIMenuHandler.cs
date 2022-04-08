using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuHandler : MonoBehaviour
{
    public static UIMenuHandler instance;

    [SerializeField] GameEvent OnDialogueEnd;

    [SerializeField] Canvas DialogueBar;
    public Text DialogueBarText;

    private void Awake()
    {
        instance = this;
    }

    //Displays dialogue bar when chef is center stage - comes from MoveChef coroutine in base chef class
    public void DisplayDialogue()
    {
        DialogueBar.gameObject.SetActive(true);
        StartCoroutine(WaitForInput());
    }

    //wait for user to click enter
    IEnumerator WaitForInput()
    {
        while(DialogueBar.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueBar.gameObject.SetActive(false);
                OnDialogueEnd.Raise();
            }

            yield return null;
        }
    }

}
