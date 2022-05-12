using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent OnEyesOpen;
    [SerializeField] GameEvent OnEyesClosed;

    [Header("Start Menu")]
    [SerializeField] Canvas MenuCanvas;
    [SerializeField] Animator eyesAnim;
    [SerializeField] GameObject logo;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject blurImage;
    bool blinkBool;
    bool eyesClosed;

    [Header("Notice")]
    [SerializeField] Canvas NoticeCanvas;
    [SerializeField] Animator noticeAnim;
    [SerializeField] GameObject PressEnterText;

    private void Start() //initializes start of game
    {
        NoticeCanvas.gameObject.SetActive(true);
        MenuCanvas.gameObject.SetActive(false);
        StartCoroutine(WaitForInput()); //waits for user to click return key to fade out the start-game notice message
    }

    public void Reset() //resets the game
    {
        NoticeCanvas.gameObject.SetActive(false);
        StartCoroutine(DelayedReset());
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(3); //controls how long the "eyes stay closed" before start menu displays
        eyesAnim.SetTrigger("Reset");
        MenuCanvas.gameObject.SetActive(true);
        blurImage.SetActive(true);
        logo.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
    }

    //Fade the notice message when player presses enter and display the start menu
    IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(2);
        PressEnterText.GetComponent<EnterHelper>().LerpSpeed = 0.7f;

        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                noticeAnim.SetTrigger("Fade");
                yield return new WaitForSeconds(3);
                NoticeCanvas.gameObject.SetActive(false);
                MenuCanvas.gameObject.SetActive(true);
                yield break;
            }
            yield return null;
        }
    }

    //if eyes are open, close them, otherwise open them
    public void Blink()
    {
        blinkBool = blinkBool == true ? false : true;

        if (blinkBool == true)
            eyesClosed = true;
        else
            eyesClosed = false;

        eyesAnim.SetBool("Blink", blinkBool); //if blinkBool is false, the "Eyes Opening" animation plays

        //hide the logo and start button
        logo.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        //if eyes are closing, follow with eyesOpening animation sequence
        if (eyesClosed == true)
        {
            OnEyesClosed.Raise(); //gets picked up by the RenderManager
            StartCoroutine(AbreLosOjos());
        }
    }

    //opens eyes
    IEnumerator AbreLosOjos()
    {
        yield return new WaitForSeconds(4);
        //play alien sound fx
        blurImage.SetActive(false);

        Blink();

        eyesClosed = false;
        yield return new WaitForSeconds(4.5f);
        //eyesAnim.enabled = false;
        OnEyesOpen.Raise();
    }

    //response to OnPlayerIsRunning triggered in PlayerController
    //Renders the UI elements
    public void SetupBlinker()
    {
        //Debug.Log("Blink UI Rendered");
        MenuCanvas.gameObject.SetActive(true);
        eyesAnim.gameObject.SetActive(true);
        eyesAnim.enabled = true;
    }

    //response to OnPlayerHasFallen triggered in PlayerController
    //UI "blinks" and game is reset
    public void EndGameBlink()
    {
        eyesAnim.SetTrigger("Blink_endgame_start");
    }
}
