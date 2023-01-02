using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;

public class EscMenu : MonoBehaviour
{
    public float endPosY;
    public float startPosY;

    public bool hidden;
    public bool inplace;

    [SerializeField] GameObject firstSelected;


    void Start()
    {
        inplace = false;
        hidden = true;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) && hidden == true && inplace == false)
    //    {
    //        var test = transform.DOMoveY(endPosY, 0.6f).OnComplete(Complete).SetEase(Ease.OutCirc);
    //        test.SetUpdate(true);
    //        Time.timeScale = 0;

    //        if (endPosY <= 0f)
    //        {
    //            inplace = true;
    //        }
    //    }
    //    if (Input.GetKeyDown(KeyCode.Escape) && hidden == false && inplace == true)
    //    {
    //        var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
    //        test.SetUpdate(true);
    //        Time.timeScale = 1;

    //        if (startPosY >= 10f)
    //        {
    //            inplace = false;
    //        }
    //    }
    //}

    //void Complete()
    //{
    //    hidden = !hidden;
    //}
    //public void Resume()
    //{
    //    if (hidden == false && inplace == true)
    //    {
    //        var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
    //        test.SetUpdate(true);
    //        Time.timeScale = 1;

    //        if (startPosY >= 10f)
    //        {
    //            inplace = false;
    //        }
    //    }
    //}

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SetNavigatingPlayer(MultiplayerEventSystem eventSys)
    {
        if (eventSys != null)
            eventSys.SetSelectedGameObject(firstSelected);
    }

    public void TogglePause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    //    if (hidden == true && inplace == false)
    //    {
    //        var test = transform.DOMoveY(endPosY, 0.6f).OnComplete(Complete).SetEase(Ease.OutCirc);
    //        test.SetUpdate(true);
    //        Time.timeScale = 0;

    //        if (endPosY <= 0f)
    //        {
    //            inplace = true;
    //        }
    //    }
    //    if (hidden == false && inplace == true)
    //    {
    //        var test = transform.DOMoveY(startPosY, 0.6f).OnComplete(Complete).SetEase(Ease.InCirc);
    //        test.SetUpdate(true);
    //        Time.timeScale = 1;

    //        if (startPosY >= 10f)
    //        {
    //            inplace = false;
    //        }
    //    }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
