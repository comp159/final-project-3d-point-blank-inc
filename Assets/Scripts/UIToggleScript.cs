using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIToggleScript : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject winText;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject gameOverPanel;
    private UIManagementScript uis;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        winText.SetActive(false);
        playAgainButton.SetActive(false);
        quitButton.SetActive(false);
        GameObject temp = GameObject.FindGameObjectWithTag("PauseScreen");
        uis = temp.GetComponent<UIManagementScript>();
        ui.SetActive(false);
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PauseGame();
        }

        if (player.get_cur_health() <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void PauseGame()
    {
        if (ui.activeInHierarchy)
        {
            ui.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            ui.SetActive(true);
            uis.UpdateAll();
            Time.timeScale = 0;
        }
    }

    public void DisplayWin()
    {
        winText.SetActive(true);
        playAgainButton.SetActive(true);
        quitButton.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName: "Title Scene");
    }
}
