using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasHandler : MonoBehaviour
{
    public string RoundText { get => roundText.text; set => roundText.text = value; }
    [SerializeField] TextMeshProUGUI roundText;

    void Start()
    {
        GameManager.Instance.AddCanvasHandler(this);
    }

    public void StartWinScreen(string playerName)
    {

    }
}
