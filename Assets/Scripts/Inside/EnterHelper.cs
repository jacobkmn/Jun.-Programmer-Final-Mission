using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>
/// defines behavior for the dialogue UI Canvas to aid the player with input
///</summary>
public class EnterHelper : MonoBehaviour
{
    Text text;
    //inspector selectors for color change speed, dif colors, and string entry
    [SerializeField] [Range(0f, 1f)] float lerpSpeed;
    [SerializeField] public Color[] colors;
    [SerializeField] string textToDisplay;

    //variables to cycle through index array
    int colorIndex = 0;
    float t = 0f;

    void Awake()
    {
        text = GetComponent<Text>();
        text.text = textToDisplay;
    }

    void OnEnable()
    {
        StartCoroutine(ColorShifter());
    }

    void OnDisable()
    {
        StopCoroutine(ColorShifter());
    }

    IEnumerator ColorShifter()
    {
        while(gameObject.activeInHierarchy)
        {
            LerpColor();
            yield return null;
        }
    }

    void LerpColor()
    {
        //lerps the t variables, used below
        t = Mathf.MoveTowards(t, 1.0f, lerpSpeed * Time.deltaTime);
        text.color = Color.Lerp(text.color, colors[colorIndex], t);

        if (t > .9f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= colors.Length) ? 0 : colorIndex;
        }
    }

}
