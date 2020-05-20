using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TalkGoal")]
public class TalkGoal : Goal
{
    public NPC npc;

    public void Talk(NPC npc)
    {
        if (npc == this.npc)
        {
            completed = true;
        }
    }
}
