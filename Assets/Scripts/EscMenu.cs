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
    public float startPosY;

    public bool hidden;
    public bool inplace;


    // Start is called before the first frame update
    void Start()
    {
        inplace = false;
        hidden = true;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Escape) && hidden == true && inplace == false)
        //{
        //    var test = transform.DOMoveY(endPosY, 0.6f).OnComplete(Complete).SetEase(Ease.OutCirc);
        //    test.SetUpdate(true);
        //    Time.timeScale = 0;

        //    if (endPosY <= 0f)
        //    {
        //        inplace = true;
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Escape) && hidden == false && inplace == true)
        //{
        //    var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
        //    test.SetUpdate(true);
        //    Time.timeScale = 1;

        //    if (startPosY >= 10f)
        //    {
        //        inplace = false;
        //    }
        //}
    }

    void Complete()
    {
        hidden = !hidden;
    }
    public void Resume()
    {
        if (hidden == false && inplace == true)
        {
            var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
            test.SetUpdate(true);
            Time.timeScale = 1;

            if (startPosY >= 10f)
            {
                inplace = false;
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnEscape()
    {
        if (hidden == true && inplace == false)
        {
            var test = transform.DOMoveY(endPosY, 0.6f).OnComplete(Complete).SetEase(Ease.OutCirc);
            test.SetUpdate(true);
            Time.timeScale = 0;

            if (endPosY <= 0f)
            {
                inplace = true;
            }
        }
        if (hidden == false && inplace == true)
        {
            var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
            test.SetUpdate(true);
            Time.timeScale = 1;

            if (startPosY >= 10f)
            {
                inplace = false;
            }
        }
    }
}
