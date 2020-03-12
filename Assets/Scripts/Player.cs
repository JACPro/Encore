using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    string name;

    public string getName() 
    {
        return name;        
    }

    public void setName(string name) 
    {
        this.name = name;
    }
}
