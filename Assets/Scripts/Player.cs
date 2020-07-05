using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject questUI;

    [SerializeField]
    GameObject pauseMenu;

    string name;

    public string getName() 
    {
        return name;        
    }

    public void setName(string name) 
    {
        this.name = name;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q) & questUI != null) {
            questUI.SetActive(!questUI.activeSelf);
            if (questUI.activeSelf || FindObjectOfType<DialogueManager>().GetInDialogue())
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(FindObjectOfType<DialogueManager>().GetInDialogue());
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = 1 - Convert.ToInt32(pauseMenu.activeSelf);
            if (pauseMenu.activeSelf || FindObjectOfType<DialogueManager>().GetInDialogue() || questUI.activeSelf) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            } else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
