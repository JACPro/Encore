using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string getName() 
    {
        return itemName;
    }

    public string getDescription()
    {
        return description;
    }
}
