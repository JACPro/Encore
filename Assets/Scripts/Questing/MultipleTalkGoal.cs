using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MultipleTalkGoal")]
public class MultipleTalkGoal : Goal
{
    [SerializeField]
    string faction;

    [SerializeField]
    int goal;

    int current = 0;

    public void Talk(string faction)
    {
        if (faction == this.faction)
        {
            current++;
            if (current == goal) {
                completed = true;
                quest.IsComplete();
            }
        }
    }
}
