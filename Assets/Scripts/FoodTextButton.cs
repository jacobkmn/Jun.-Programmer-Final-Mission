using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTextButton : MonoBehaviour
{

    [SerializeField] Button button;
    [SerializeField] Text designatedFoodItem;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        UIMenuHandler.instance.DisplayResponseDialogue();

        RelaySelectedFood();
    }

    void RelaySelectedFood()
    {
        if (designatedFoodItem.text == "Chocolate Chip" || designatedFoodItem.text == "Chocolate Chip")
        {
            FoodDisplay.instance.SelectedFood = "Cookie";
        }
        else
            FoodDisplay.instance.SelectedFood = designatedFoodItem.text;
    }
}
