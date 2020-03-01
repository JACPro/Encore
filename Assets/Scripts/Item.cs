using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemName;
    string description;

    public Item(string itemName, string description) 
    {
        this.itemName = itemName;
        this.description = description;
    }

    public string getName() 
    {
        return itemName;
    }

    public string getDescription()
    {
        return description;
    }
}
