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
    [SerializeField] Animator DialogueAnim;
    [SerializeField] Canvas DialogueBarCanvas;
    public Image DialogueBarImage;
    public Text DialogueBarText;
    [SerializeField] Text enterHelperText;

    [Header("Food Menu")]
    public Animator FoodMenuAnimator;
    [SerializeField] Canvas FoodMenuCanvas;
    [SerializeField] List<Text> FoodTextOptions;
    List<Transform> ActiveButtons = new List<Transform>();

    [Header("Food Display")]
    [SerializeField] Canvas foodDisplayCanvas;
    public Canvas FoodDisplayCanvas
    {
        get { return foodDisplayCanvas; }
    }

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

    //Initialize Dialogue data and wait for player input
    IEnumerator InitialDialogue()
    {
        DialogueBarImage.sprite = ActiveChefSprite();
        DialogueBarText.text = InitialDialogueString();
        ActivateButtons();
        DialogueBarCanvas.gameObject.SetActive(true);
        DialogueAnim.SetBool("Reveal", true);
        enterHelperText.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueAnim.SetBool("Reveal", false);
                yield return new WaitForSeconds(0.33f); //the time it takes for animation to complete
                DialogueBarCanvas.gameObject.SetActive(false);
                enterHelperText.gameObject.SetActive(false);
                FoodMenuCanvas.gameObject.SetActive(true);
                FoodMenuAnimator.SetBool("Reveal_hand", true);
            }
            yield return null;
        }
    }

    IEnumerator ResponseDialogue()
    {
        FoodMenuAnimator.SetBool("Reveal_hand", false);
        yield return new WaitForSeconds(1);
        FoodMenuCanvas.gameObject.SetActive(false);
        DialogueBarText.text = ResponseDialogueString();
        DialogueBarCanvas.gameObject.SetActive(true);
        DialogueAnim.SetBool("Reveal", true);
        enterHelperText.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                DialogueAnim.SetBool("Reveal", false);
                yield return new WaitForSeconds(0.33f);
                DialogueBarCanvas.gameObject.SetActive(false);
                enterHelperText.gameObject.SetActive(false);
                OnOrderPlaced.Raise(); //starts chef sequence. Listener attached to chefs in inspector
                orderBeingPlaced = true; //gets picked up by chef class, at bottom of MoveChef coroutine
            }
            yield return null;
        }
    }

    IEnumerator DeliveryDialogue()
    {
        DeActivateButtons();
        orderBeingPlaced = false;
        DialogueBarText.text = DeliveryDialogueString();
        DialogueBarCanvas.gameObject.SetActive(true);
        DialogueAnim.SetBool("Reveal", true);
        enterHelperText.gameObject.SetActive(true);

        while (DialogueBarCanvas.gameObject.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LightBehavior.instance.Dim(); //Dims light for food to be displayed
                DialogueAnim.SetBool("Reveal", false);
                yield return new WaitForSeconds(0.33f);
                DialogueBarCanvas.gameObject.SetActive(false);
                enterHelperText.gameObject.SetActive(false);

                yield return new WaitForSeconds(1);

                foodDisplayCanvas.gameObject.SetActive(true);
                FoodDisplay.instance.ActivateFood();
                OnDialogueEnd.Raise(); //starts chef sequence. Listener attached to chef in inspector
            }
            yield return null;
        }
    }

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

    Sprite ActiveChefSprite()
    {
        return ChefReader.instance.currentChef.chefData.DesignatedImage;
    }

    //loops thrpugh available food options in the active chef's chefdata
    //activates a button for each option and sets the text = the food option name
    void ActivateButtons()
    {
        for (int i = 0; i < ChefReader.instance.currentChef.chefData.AvailableFoodNames.Count; i++)
        {
            Transform button = FoodTextOptions[i].transform.parent; //the parent button to the child text object in this list
            ActiveButtons.Add(button);
            button.gameObject.SetActive(true);
            FoodTextOptions[i].text = ChefReader.instance.currentChef.chefData.AvailableFoodNames[i];
        }
    }

    void DeActivateButtons()
    {
        for (int i = 0; i < ActiveButtons.Count; i++)
        {
            Transform button = ActiveButtons[i]; 
            button.gameObject.SetActive(false);
            button.GetComponentInChildren<Text>().text = "";
        }
        ActiveButtons.Clear();
    }
}
