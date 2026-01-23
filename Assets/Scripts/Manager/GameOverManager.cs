using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private GameObject gameOverUI;
    private GameObject gameOverTitle;
    private GameObject gameOverDescription;
    private GameObject gameOverButton;

    private void Start()
    {
        gameOverUI = GameObject.Find("GameOverUI").gameObject;
        gameOverTitle = GameObject.Find("GameOverTitle").gameObject;
        gameOverDescription = GameObject.Find("GameOverDescription").gameObject;
        gameOverButton = GameObject.Find("GameOverButton").gameObject;

        gameOverUI.SetActive(false);
        gameOverButton.SetActive(false);
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        gameOverTitle.SetActive(true);
        gameOverDescription.SetActive(true);
        Invoke("ShowGameOverButton", 3f);
    }

    private void ShowGameOverButton()
    {
        gameOverButton.SetActive(true);
    }

    public void RestartGame()
    {
        gameOverUI.SetActive(false);
        gameOverButton.SetActive(false);
        FindObjectOfType<LoadingScreenManager>().LoadScene("Lobby");
    }
}
