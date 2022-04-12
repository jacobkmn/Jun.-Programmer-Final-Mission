using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDisplay : MonoBehaviour
{

    //I think the smart way to do this would be to set up an object pooler here
    //where we read each chef's chefdata that contains their available foods
    //and instantiate one of each available food for each chef in the scene
    //from there we can have them all at the same fixed position in front of the star UI
    //and match up tags or something like that to trigger which one gets spawned, according to the button click

}
