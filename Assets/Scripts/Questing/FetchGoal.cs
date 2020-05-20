using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FetchGoal")]
public class FetchGoal : Goal
{
    public GameObject requiredItem;

    public void NewItem(GameObject item) {
        if (item == requiredItem) {
            completed = true;
            //cross off goal
        }
    }
}
