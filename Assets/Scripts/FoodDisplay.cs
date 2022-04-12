using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDisplay : MonoBehaviour
{
    public static FoodDisplay instance;

    [SerializeField] List<GameObject> PooledFood = new List<GameObject>();

    int totalFoodOptions;
    List<GameObject> availableFood = new List<GameObject>();
    List<GameObject> availableChefs = new List<GameObject>();

    string selectedFood;
    public string SelectedFood
    {
        get { return selectedFood; }
        set { selectedFood = value; }
    }

    bool chefsAccountedFor;

    GameObject activeFood;
    [SerializeField] float rotateSpeed = 5.0f;

    //big box collider reads each chef behind doors
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chef")
        {
            print("detected object: " + other.gameObject.name);
            availableChefs.Add(other.gameObject);
        }
        //print("availableChefs: " + availableChefs);
        chefsAccountedFor = true;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnAvailableFood());
    }

    IEnumerator SpawnAvailableFood()
    {
        WaitUntil waitForChefs = new WaitUntil(() => chefsAccountedFor);

        yield return waitForChefs;
        print("All chefs accounted for.");

        CalculateTotalFoodOptions();

        for (int i = 0; i < totalFoodOptions; i++)
        {
            GameObject food = Instantiate(availableFood[i], availableFood[i].transform.position, availableFood[i].transform.localRotation);
            PooledFood.Add(food);
            food.transform.SetParent(gameObject.transform);
            food.SetActive(false);
        }
    }

    void CalculateTotalFoodOptions()
    {
        for (int i = 0; i < availableChefs.Count; i++)
        {
            availableFood.AddRange(availableChefs[i].GetComponent<Chef>().chefData.AvailableFood);
            int foodOptions = availableChefs[i].GetComponent<Chef>().chefData.AvailableFood.Count;
            totalFoodOptions += foodOptions;
        }
    }

    public void ActivateFood()
    {
        foreach (GameObject food in PooledFood)
        {
            if (food.tag == SelectedFood)
            {
                food.SetActive(true);
                activeFood = food;
            }
        }
        StartCoroutine(RotateFood());
    }

    public void DeActivateFood()
    {

    }

    IEnumerator RotateFood()
    {
       while (activeFood.gameObject.activeInHierarchy)
        activeFood.transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);

        yield return null;
    }

}
