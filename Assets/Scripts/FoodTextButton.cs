using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTextButton : MonoBehaviour
{

    [SerializeField] Button button;

    void Start()
    {
        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        UIMenuHandler.instance.DisplayResponseDialogue();
        //print("test");
    }

    void DetermineFoodType()
    {
        //need a way to pass to this function which chef is active according to which door was clicked

        //switch ()
        //{
        //    default:
        //        print("No valid chef");
        //        break;
        //}
    }
}
