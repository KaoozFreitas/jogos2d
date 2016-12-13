using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    private GameObject mainMenuPanel;
    private GameObject creditsPanel;
    // Use this for initialization
    void Start () {
        mainMenuPanel = GameObject.Find("Menu Panel");
        creditsPanel = GameObject.Find("Credits Panel");
        creditsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("B"))
        {
            hideCredits();
            System.Console.WriteLine("B button pressed");
        }
	}

    public void PlayButton () {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void showCredits()
    {
        creditsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void hideCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
