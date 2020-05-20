using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalManager : MonoBehaviour
{
    public List<Goal> activeGoals = new List<Goal>();

    [SerializeField]
    GameObject textBox;
    TextMeshProUGUI textBoxTmp;

    //the maximum number of goals you can have per quest
    int maxGoals = 3;

    float paddingBetweenGoals = 10f;
}
