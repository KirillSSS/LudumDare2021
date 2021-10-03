using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Paues_end : MonoBehaviour
{

    public bool PauseGame, end;
    public GameObject pauseGameMenu, endOfGamePerson, endOfGameBoiler, win_end;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !end)
        {
            Debug.Log("lol");
            if (PauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
        PauseGame = false;

    }

    public void Pause()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0;
        PauseGame = true;

    }

    public void endOfGamePer()
    {
        endOfGamePerson.SetActive(true);
        Time.timeScale = 0;
        end = true;
    }

    public void endOfGameBoi()
    {
        endOfGameBoiler.SetActive(true);
        Time.timeScale = 0;
        end = true;
    }

    public void win()
    {
        win_end.SetActive(true);
        Time.timeScale = 0;
        end = true;

    }

}
