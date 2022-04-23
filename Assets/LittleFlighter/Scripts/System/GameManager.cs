using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenu;

    private List<Canvas> allCanvases;
    private float lastTimeScale;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void PauseGame()
    {
        if (this.pauseMenu == null)
        {
            throw new Exception("No pause menu was given!");
        }

        if (Time.timeScale > 0)
        {
            allCanvases = new List<Canvas>();

            lastTimeScale = Time.timeScale;
            Time.timeScale = 0;

            foreach (var canvas in GameObject.FindObjectsOfType<Canvas>())
            {
                allCanvases.Add(canvas);
                canvas.gameObject.SetActive(false);
            }

            this.pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = lastTimeScale;

            foreach (var canvas in allCanvases)
            {
                print(canvas.gameObject.name);
                canvas.gameObject.SetActive(true);
            }

            this.pauseMenu.gameObject.SetActive(false);

        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
