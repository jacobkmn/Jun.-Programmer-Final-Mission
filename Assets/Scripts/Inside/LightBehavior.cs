using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public static LightBehavior instance;

    [SerializeField] Light sceneLight;

    float originalIntensity, targetIntensity;
    float current, target;
    [SerializeField] Color[] EndGameLerp;

    private void Awake()
    {
        instance = this;
    }

    // Array of random values for the intensity.
    private float[] smoothing = new float[9];

    private void Start()
    {
        originalIntensity = sceneLight.intensity;
        targetIntensity = 1;

        // Initialize the array.
        for (int i = 0; i < smoothing.Length; i++)
        {
            smoothing[i] = .0f;
        }
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
        StartCoroutine(LightFlicker(10.0f));
        StartCoroutine(ColorLerp(10.0f));
    }

    //endgame sequence
    IEnumerator LightFlicker(float timeFlickering)
    {
        //yield return new WaitForSeconds(1f);
        float startTime = Time.time;

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


            yield return null;
        }

        sceneLight.intensity = originalIntensity - 0.5f;
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
}
