using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class EscMenu : MonoBehaviour
{
    public float endPosY;
    private float startPosY;

    public bool hidden;


    // Start is called before the first frame update
    void Start()
    {
        startPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && hidden == true)
        {
            var test = transform.DOMoveY(endPosY, 0.6f).OnComplete(Complete).SetEase(Ease.OutCirc);
            test.SetUpdate(true);
            Time.timeScale = 0;
            Debug.Log("You paused");

        }
        if (Input.GetKeyDown(KeyCode.Escape) && hidden == false)
        {
            transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
            Time.timeScale = 1;
            Debug.Log("You unpaused");
        }
    }

    void Complete()
    {
        hidden = !hidden;
    }
    public void Resume()
    {
        transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
        Time.timeScale = 1;
        Debug.Log("pressed");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
