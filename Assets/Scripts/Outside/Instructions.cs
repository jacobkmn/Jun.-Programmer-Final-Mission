using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{

    [SerializeField] Canvas InstructionsCanvas;
    [SerializeField] Image keys;
    [SerializeField] float timeUntil = 4f;

    Vector3 startPosition;

    float timer;

    private void Start()
    {
        startPosition = Camera.main.transform.position;
        keys.gameObject.SetActive(false);
        StartCoroutine(Timer());
    }

    private void Update()
    {
        //if the timer hasn't hit the time threshold and the player hasn't moved, give them a hint
        if (timer > timeUntil && Camera.main.transform.position == startPosition)
        {
            keys.gameObject.SetActive(true);
            StopCoroutine(Timer());
            timer = 0;
        }
        else if (Camera.main.transform.position != startPosition)
        {
            StopAllCoroutines();
            keys.gameObject.SetActive(false);
            timer = 0;
        }
    }

    IEnumerator Timer()
    {
        while(true)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
