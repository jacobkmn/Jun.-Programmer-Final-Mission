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

    //passes data to the Food Display class on which food the user ordered (clicked)
    void RelaySelectedFood()
    {
        if (designatedFoodItem.text == "Chocolate Chip" || designatedFoodItem.text == "Oatmeal Raisin")
        {
            FoodDisplay.instance.SelectedFood = "Cookie";
        }
        else if (designatedFoodItem.text == "Turkey Avocado Melt" || designatedFoodItem.text == "BLT")
        {
            FoodDisplay.instance.SelectedFood = "Sandwich";
        }
        else if (designatedFoodItem.text == "Slice o' Pizza")
        {
            FoodDisplay.instance.SelectedFood = "Pizza";
        }
        else
            FoodDisplay.instance.SelectedFood = designatedFoodItem.text;
    }
}
