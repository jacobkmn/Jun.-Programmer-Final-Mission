using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuHandler : MonoBehaviour
{
    public static UIMenuHandler instance;

    [Header("Events")]
    public GameEvent OnOrderPlaced;
    public GameEvent OnDialogueEnd;

    [Header("Dialogue Bar")]
    [SerializeField] Canvas DialogueBarCanvas;
    public Image DialogueBarImage;
    public Text DialogueBarText;
    [Header("Food Menu")]
    public Animator FoodMenuAnimator;
    public Text[] FoodTextOptions;

    bool orderBeingPlaced;
    public bool OrderBeingPlaced
    {
        get { return orderBeingPlaced; }
        set { orderBeingPlaced = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    //Displays dialogue bar when chef is center stage - comes from MoveChef coroutine in base chef class
    public void DisplayInitialDialogue()
    {
        StartCoroutine(InitialDialogue());
    }

    public void DisplayResponseDialogue()
    {
        StartCoroutine(ResponseDialogue());
    }

    public void DisplayDeliveryDialogue()
    {
        StartCoroutine(DeliveryDialogue());
    }

    //wait for user to click enter
    IEnumerator InitialDialogue()
    {
        DialogueBarText.text = InitialDialogueString();
        DialogueBarCanvas.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueBarCanvas.gameObject.SetActive(false);
                FoodMenuAnimator.SetBool("Reveal_hand", true);
            }
            yield return null;
        }
    }

    IEnumerator ResponseDialogue()
    {
        FoodMenuAnimator.SetBool("Reveal_hand", false);
        yield return new WaitForSeconds(1);
        DialogueBarText.text = ResponseDialogueString();
        DialogueBarCanvas.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueBarCanvas.gameObject.SetActive(false);
                OnOrderPlaced.Raise(); //starts chef sequence. Listener attached to chefs in inspector
                orderBeingPlaced = true; //gets picked up by chef class, at bottom of MoveChef coroutine
            }
            yield return null;
        }
    }

    IEnumerator DeliveryDialogue()
    {
        orderBeingPlaced = false;
        DialogueBarText.text = DeliveryDialogueString();
        DialogueBarCanvas.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueBarCanvas.gameObject.SetActive(false);
                OnDialogueEnd.Raise(); //starts chef sequence. Listener attached to chef in inspector
            }
            yield return null;
        }
    }

    //Probably going to run into a problem here of all chefs listening to ondialogueend simultaneously.
    //Lets cross that bridge when we get there
    string InitialDialogueString()
    {
        return ChefReader.instance.currentChef.chefData.InitialDialogueOption;
    }
    string ResponseDialogueString()
    {
        return ChefReader.instance.currentChef.chefData.ResponseDialogueOption;
    }

    string DeliveryDialogueString()
    {
        return ChefReader.instance.currentChef.chefData.DeliveryDialogueOption;
    }
}
