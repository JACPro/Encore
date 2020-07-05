using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FetchGoal")]
public class FetchGoal : Goal
{
    public string requiredItem;

    public void NewItem(string item) {
        if (item == requiredItem) {
            completed = true;
            quest.IsComplete();            
        }
    }
}
