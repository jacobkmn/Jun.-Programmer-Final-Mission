using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Chef Data", menuName = "Chef Data")]
public class ChefData : ScriptableObject
{
    [SerializeField] Sprite designatedImage;
    public Sprite DesignatedImage
    {
        get { return designatedImage; }
    }

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

    [SerializeField] List<string> deliveryDialogueOptions = new List<string>();
    public string DeliveryDialogueOption
    {

        get { return deliveryDialogueOptions[Random.Range(0, deliveryDialogueOptions.Count)]; }

        set { deliveryDialogueOptions[Random.Range(0, deliveryDialogueOptions.Count)] = value; }

    }

    [SerializeField] List<string> availableFoodNames = new List<string>();
    public List<string> AvailableFoodNames
    {
        get { return availableFoodNames; }
    }

    [SerializeField] List<GameObject> availableFood = new List<GameObject>();
    public List<GameObject> AvailableFood
    {
        get { return availableFood; }
    }
}
