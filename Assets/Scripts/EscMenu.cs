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
            transform.DOMoveY(endPosY, 1).OnComplete(Complete).SetEase(Ease.OutBounce);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && hidden == false)
        {
            transform.DOMoveY(startPosY, 1).OnComplete(Complete).SetEase(Ease.InBounce);
        }
    }

    void Complete()
    {
        hidden = !hidden;
    }
    public void Resume()
    {
        transform.DOMoveY(startPosY, 1).OnComplete(Complete).SetEase(Ease.InBounce);
        Debug.Log("pressed");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
