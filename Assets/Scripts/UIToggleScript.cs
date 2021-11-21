using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleScript : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject winText;
    
    private UIManagementScript uis;
    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        GameObject temp = GameObject.FindGameObjectWithTag("PauseScreen");
        uis = temp.GetComponent<UIManagementScript>();
        ui.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (ui.activeInHierarchy == true)
        {
            ui.SetActive(false);
            Time.timeScale = 1;
        }
        else if (ui.activeInHierarchy == false)
        {
            ui.SetActive(true);
            uis.UpdateAll();
            Time.timeScale = 0;
        }
    }

    public void DisplayWin()
    {
        winText.SetActive(true);
        //display the replay & quit buttons here
        Time.timeScale = 0;
    }
}
