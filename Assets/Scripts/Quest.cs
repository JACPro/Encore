using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {

    bool isActive;

    bool isComplete;

    string title;
    string description;    


    /*
    Accessor and mutator methods for private quest variables
    */
    public bool getActive()
    {
        return isActive;
    }

    public void setActive(bool newActive) 
    {
        isActive = newActive;
    }

    public bool getComplete()
    {
        return isComplete;
    }

    public void setComplete(bool newCompleted)
    {
        isComplete = newCompleted;
    }

    public string getTitle()
    {
        return title;
    }

    public void setTitle(string title)
    {
        this.title = title;
    }

    public string getDescription()
    {
        return description;
    }

    public void setDescription(string description)
    {
        this.description = description;
    }    
}
