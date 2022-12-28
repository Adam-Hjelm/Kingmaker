using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PunchScript : MonoBehaviour
{
    public Vector3 punch = new Vector3(0.5f, 0.5f, 0.5f);
    public float duration = 0.5f;
    public int vibrato = 5;
    public float elasticity = 2;

    private Vector3 originalLocalScale;

    private void Awake()
    {
        originalLocalScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = originalLocalScale;
        transform.DOPunchScale(punch, duration, vibrato, elasticity);
    }

    private void OnDisable()
    {
        transform.localScale = originalLocalScale;
        transform.DOKill();
    }
}
