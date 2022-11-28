using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void settings()
    {
        //SceneManager.LoadScene("Settings);
    }
}
