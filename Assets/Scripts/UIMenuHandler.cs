using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuHandler : MonoBehaviour
{
    public static UIMenuHandler instance;

    [SerializeField] Canvas DialogueBar;
    public Text DialogueBarText;

    private void Awake()
    {
        instance = this;
    }

    public void DisplayDialogue()
    {
        DialogueBar.gameObject.SetActive(true);
    }


}
