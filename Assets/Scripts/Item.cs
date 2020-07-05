using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string itemName;
    [SerializeField] string description;

    public string GetName() 
    {
        return itemName;
    }

    public string GetDescription()
    {
        return description;
    }
}
