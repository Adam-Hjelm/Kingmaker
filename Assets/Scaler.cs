using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    public float duration = 1;
    Vector3 originalSize;


    void Awake()
    {
        originalSize = transform.localScale;
    }

    public void StartAnimation()
    {
        transform.localScale = originalSize;
        transform.DOScale(0, duration);
    }
}
