using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuHandler : MonoBehaviour
{
    public static UIMenuHandler instance;

    public GameEvent OnDialogueEnd;

    [Header("Dialogue Bar")]
    [SerializeField] Canvas DialogueBarCanvas;
    public Text DialogueBarText;
    [Header("Food Menu")]
    public Animator FoodMenuAnimator;
    public Text[] FoodTextOptions;

    private void Awake()
    {
        instance = this;
    }

    //Displays dialogue bar when chef is center stage - comes from MoveChef coroutine in base chef class
    public void DisplayDialogue()
    {
        DialogueBarCanvas.gameObject.SetActive(true);
        StartCoroutine(WaitForInput());
    }

    //wait for user to click enter
    IEnumerator WaitForInput()
    {
        while(DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueBarCanvas.gameObject.SetActive(false);
                FoodMenuAnimator.SetBool("Reveal_hand", true);
            }

            yield return null;
        }
    }
}
