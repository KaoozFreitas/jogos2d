﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseBehaviourScript : MonoBehaviour
{
    private GameObject gameOverPanel;
    private GameObject pausePanel;

    bool isPaused; //Used to determine paused state

    void Start()
    {
        pausePanel = GameObject.Find("Pause Panel");
        pausePanel.SetActive(false); //make sure our pause menu is disabled when scene starts


        gameOverPanel = GameObject.Find("Game Over Panel");
        gameOverPanel.SetActive(false); 

        isPaused = false; //make sure isPaused is always false when our scene opens
    }

    void Update()
    {
        System.Console.WriteLine("checking");
        //If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
        if (Input.GetButtonDown("Pause") && !isPaused)
            Pause();
        else if (Input.GetButtonDown("Pause") && isPaused)
            UnPause();

        if(NoMorePlayers())
        {
            GameOver();
        }
    }

    public bool NoMorePlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        return players.Length <= 0;
    }

    public void Pause()
    {
        isPaused = true;
        pausePanel.SetActive(true); //turn on the pause menu
        Button continueButton = GameObject.Find("Continue Button").GetComponent<Button>();
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
        Time.timeScale = 0f; //pause the game
    }

    void GameOver()
    {
        isPaused = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; //pause the game
    }

    public void UnPause()
    {
        isPaused = false;
        pausePanel.SetActive(false); //turn off pause menu
        Time.timeScale = 1f; //resume game
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        this.UnPause();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}