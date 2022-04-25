using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    [SerializeField] Canvas InstructionsCanvas;
    [SerializeField] Image keys;
    [SerializeField] Image mouse;
    [SerializeField] float timeUntil = 4f;
    float timer;
    bool playerGotTheHint;

    OutsideDoors outsideDoors;

    private void Start()
    {
        outsideDoors = FindObjectOfType<OutsideDoors>();
        keys.gameObject.SetActive(false);
        mouse.gameObject.SetActive(false);
        StartTimer();
    }

    private void Update()
    {
        //if the timer hasn't hit the time threshold and the player hasn't moved, give them a hint
        if (timer > timeUntil && PlayerPosition.instance.AtStartPosition())
        {
            keys.gameObject.SetActive(true);
        }
        else if (!PlayerPosition.instance.AtStartPosition())
        {
            keys.gameObject.SetActive(false);
        }
        //timer is triggered fresh by the game event raised in the doortrigger class
        if (timer > timeUntil && outsideDoors.IsHovering == false && DoorTrigger.instance.IsTriggered && !playerGotTheHint)
        {
            mouse.gameObject.SetActive(true);
        }
        else if (outsideDoors.IsHovering == true && DoorTrigger.instance.IsTriggered)
        {
            mouse.gameObject.SetActive(false);
            playerGotTheHint = true;
        }
    }

    public void StartTimer()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        WaitUntil waitForGameStart = new WaitUntil(() => GameManager.instance.GameStarted);
        yield return waitForGameStart;

        //Debug.Log("Timer Started");
        timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public void HideAllUI()
    {
        keys.gameObject.SetActive(false);
        mouse.gameObject.SetActive(false);
        StopCoroutine(Timer());
        timer = 0;
        //Debug.Log("Timer stopped");
    }
}
