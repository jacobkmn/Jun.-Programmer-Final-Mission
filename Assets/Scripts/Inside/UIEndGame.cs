using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEndGame : MonoBehaviour
{
    [SerializeField] Button doorButton;
    [SerializeField] GameEvent OnQuitGame;
    [SerializeField] Canvas EndGameCanvas;

    [Header("Text PopUp")]
    [SerializeField] GameObject TextPopUpHolder;
    [SerializeField] Button YesButton;
    [SerializeField] Button NoButton;
    [SerializeField] float lerpSpeed = 1.0f;

    [Header("GameEvent")]
    [SerializeField] GameEvent OnExitGame;

    private void Start()
    {
        doorButton.onClick.AddListener(OnClick);
        YesButton.onClick.AddListener(Terminate);
        NoButton.onClick.AddListener(KeepPlaying);
    }

    void OnClick()
    {
        OnQuitGame.Raise(); //gameManager listens to set global bool active: block raycasts from highlighting doors
        TextPopUpHolder.gameObject.SetActive(true);
        doorButton.gameObject.SetActive(false);
        LerpCanvasGroup();
    }

    void LerpCanvasGroup()
    {
        StartCoroutine(Fader(lerpSpeed, 0)); // fades UI in, giving player option to exit or keep playing
    }

    IEnumerator Fader(float overTime, int gate)
    {
        CanvasGroup canvasGroup = TextPopUpHolder.GetComponent<CanvasGroup>();
        float currentAlpha, targetAlpha;
        currentAlpha = canvasGroup.alpha;
        targetAlpha = currentAlpha == 0 ? 1 : 0;

        float elapsedTime = 0;

        while (elapsedTime < overTime)
        {
            canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / overTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;

        if (gate == 1) //Player selected to exit game
        {
            EndGameCanvas.gameObject.SetActive(false);
            OnExitGame.Raise();
        }
        else if (gate == 2) //Player selected to keep playing
        {
            OnQuitGame.Raise(); //gameManager sets global bool to false
            TextPopUpHolder.gameObject.SetActive(false);
            doorButton.gameObject.SetActive(true); //door icon shows up after the textPopup dissapears
        }
    }

    //endgame canvas becomes active as soon as the first chef sequence completes.
    //Essentially, gives player option to exit
    //Listener in inspector: responds to OnNestedChef
    public void BecomeActive()
    {
        EndGameCanvas.gameObject.SetActive(true);
    }
    //Listener in inspector: responds to OnDoorClicked
    public void DisActivate()
    {
        EndGameCanvas.gameObject.SetActive(false);
    }

    void Terminate()
    {
        //Picked up by LightBehavior class to start endgame sequence
        //OnExitGame.Raise();
        doorButton.gameObject.SetActive(false); //no idea why, but the door icon shows up after clicking "NO". This line prevents that.
        StartCoroutine(Fader(lerpSpeed, 1)); // closes out UI
    }

    void KeepPlaying()
    {
        doorButton.gameObject.SetActive(false); 
        StartCoroutine(Fader(lerpSpeed, 2)); // fades out UI and lets gamemanager know we'll keep playing
    }
}
