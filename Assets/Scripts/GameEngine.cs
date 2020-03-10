using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    public GameObject pausePanel;    
    public void StartButtonTapped()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButtonTapped()
    {
        Application.Quit();
    }
    public void PauseButtonTapped()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeButtonTapped()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartButtonTapped()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenuButtonTapped()
    {
        pausePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
