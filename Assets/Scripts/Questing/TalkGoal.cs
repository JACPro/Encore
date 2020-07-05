using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TalkGoal")]
public class TalkGoal : Goal
{
    public string npc;

    public void Talk(NPC npc)
    {
        if (npc.GetName() == this.npc)
        {
            Debug.Log(quest);
            completed = true;
            quest.IsComplete();
        }
    }
}
