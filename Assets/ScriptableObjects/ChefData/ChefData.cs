using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Chef Data", menuName = "Chef Data")]
public class ChefData : ScriptableObject
{
    [SerializeField] List<string> initialDialogueOptions = new List<string>();
    public string InitialDialogueOption
    {

        get { return initialDialogueOptions[Random.Range(0, initialDialogueOptions.Count)]; }

        set { initialDialogueOptions[Random.Range(0, initialDialogueOptions.Count)] = value; }

    }

    [SerializeField] List<string> responseDialogueOptions = new List<string>();
    public string ResponseDialogueOption
    {

        get { return responseDialogueOptions[Random.Range(0, responseDialogueOptions.Count)]; }

        set { responseDialogueOptions[Random.Range(0, responseDialogueOptions.Count)] = value; }

    }
}
