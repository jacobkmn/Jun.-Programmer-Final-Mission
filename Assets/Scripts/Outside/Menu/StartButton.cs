using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    Button button;
    [SerializeField] GameEvent OnStartButtonClicked;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    //fires off event for listeners to kick things off
    void OnButtonClicked() {

        OnStartButtonClicked.Raise();
    }
}
