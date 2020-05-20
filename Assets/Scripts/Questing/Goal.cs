using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : ScriptableObject
{
    public string title;
    public string description;
    public bool completed;

    public Goal nextGoal;
    public Goal prevGoal;

    public bool IsCompleted() {
        return completed;
    }

    public void SetNextGoal(Goal goal) {
        nextGoal = goal;
        nextGoal.prevGoal = this;
    }
}
