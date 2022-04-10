using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Food Data", menuName = "Food Data")]
public class FoodData : ScriptableObject
{
    string foodName;
    public string FoodName
    {
        get { return foodName; }
        set { foodName = value; }
    }
}