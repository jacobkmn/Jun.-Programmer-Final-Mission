using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour
{
    public static LightBehavior instance;

    [SerializeField] Light sceneLight;

    float originalIntensity, targetIntensity;
    float current, target;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originalIntensity = sceneLight.intensity;
        targetIntensity = 1;
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
            sceneLight.intensity = Mathf.Lerp(originalIntensity, targetIntensity, current);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
