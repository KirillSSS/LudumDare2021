using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_Button : MonoBehaviour
{

    public void Scenes(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void Exit()
    {
        Debug.Log("SCUKO!");
        Application.Quit();
    }

}
