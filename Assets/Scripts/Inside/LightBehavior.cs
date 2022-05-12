using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public static LightBehavior instance;

    [SerializeField] Light sceneLight;
    [SerializeField] Light moonLight;
    public Light MoonLight
    {
        get { return moonLight; }
    }

    Color originalColor;
    float originalIntensity, targetIntensity;
    float current, target;
    [SerializeField] Color[] EndGameLerp;

    [Header("Game Events")]
    [SerializeField] GameEvent OnLightsFlickering;
    [SerializeField] GameEvent OnLightDoneFlickering;

    // Array of random values dictate intensity of flicker. Smaller the array = more flickering
    private float[] smoothing = new float[9];

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalIntensity = sceneLight.intensity;
        originalColor = sceneLight.color;
        targetIntensity = 1;

        // Initialize the array.
        for (int i = 0; i < smoothing.Length; i++)
        {
            smoothing[i] = .0f;
        }
    }

    public void Reset()
    {
        sceneLight.intensity = originalIntensity;
        sceneLight.color = originalColor;
        //moonLight.gameObject.SetActive(true);
        StopAllCoroutines();
    }

    public void Dim()
    {
        StartCoroutine(DimLerp(1.0f));
    }

    IEnumerator DimLerp(float time)
    {
        target = target == 0 ? 1 : 0;

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            current = Mathf.MoveTowards(current, target, Time.deltaTime);
            sceneLight.intensity = Mathf .Lerp(originalIntensity, targetIntensity, current);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void FlashingLights()
    {
        StartCoroutine(LightFlicker(9.0f));
        StartCoroutine(ColorLerp(9.0f));
    }

    //endgame sequence
    IEnumerator LightFlicker(float timeFlickering)
    {
        //yield return new WaitForSeconds(1f);
        float startTime = Time.time;
        int loopCount = 0;

        while (Time.time < startTime + timeFlickering )
        {
            float sum = .0f;

            // Shift values in the table so that the new one is at the
            // end and the older one is deleted.
            for (int i = 1; i < smoothing.Length; i++)
            {
                smoothing[i - 1] = smoothing[i];
                sum += smoothing[i - 1];
            }

            // Add the new value at the end of the array.
            smoothing[smoothing.Length - 1] = Random.Range(1.0f, 4.0f);
            sum += smoothing[smoothing.Length - 1];

            // Compute the average of the array and assign it to the
            // light intensity.
            sceneLight.intensity = sum / smoothing.Length;

            //when flickering is about 3/4 of the way done, triggers door opening
            if (loopCount == 8)
            {
                OnLightsFlickering.Raise();
            }
            loopCount++;
            yield return null;
        }

        sceneLight.intensity = originalIntensity - 0.5f;
        OnLightDoneFlickering.Raise(); //triggers chefs to move to center-stage & playercontroller to start coroutine
    }

    IEnumerator ColorLerp(float overTime)
    {
        float startTime = Time.time;

        while (Time.time < startTime + overTime)
        {
            sceneLight.color = Color.Lerp(EndGameLerp[0], EndGameLerp[1], (Time.time - startTime) / overTime);
            yield return null;
        }
    }

    //Moonlight gets Deactivated by RenderManager when player is indoors
    //and reactivated when player is running in endgame sequence
}
