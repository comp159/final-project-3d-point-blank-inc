using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleScript : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    // Start is called before the first frame update
    void Start()
    {
        ui.SetActive(false);
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
            Time.timeScale = 0;
        }
        else if (ui.activeInHierarchy == false)
        {
            ui.SetActive(true);
            Time.timeScale = 1;
        }
    }
}
