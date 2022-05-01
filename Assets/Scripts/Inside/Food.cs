using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] GameEvent OnFoodEaten;

    private void OnMouseDown()
    {
        AudioManager.instance.ChangeClip("Food_munch", Camera.main.gameObject);
        AudioManager.instance.PlaySound("Food_munch");
        gameObject.SetActive(false);
        UIMenuHandler.instance.FoodDisplayCanvas.gameObject.SetActive(false);
        FoodDisplay.instance.DeActivateFood();
        LightBehavior.instance.Dim();
        OnFoodEaten.Raise();
    }
}
