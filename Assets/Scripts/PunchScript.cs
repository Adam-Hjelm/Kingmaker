using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PunchScript : MonoBehaviour
{

    public Vector3 punch =new Vector3(0.5f,0.5f,0.5f);
    public float duration = 0.5f;
    public int vibrato = 5;
    public float elasticity = 2;


    // Start is called before the first frame update
    public void OnEnable()
    {
        //transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 5, 2);
        transform.localScale = Vector3.one;
        transform.DOPunchScale(punch, duration, vibrato, elasticity);
    }
}
