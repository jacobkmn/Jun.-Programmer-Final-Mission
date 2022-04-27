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

    private void Start()
    {
        MenuCanvas.gameObject.SetActive(false);
        StartCoroutine(WaitForInput());
    }

    //Fade the notice message when player presses enter
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

        eyesAnim.SetBool("Blink", blinkBool);

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

        OnEyesOpen.Raise();
    }
}
