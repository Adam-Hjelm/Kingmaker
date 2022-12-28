using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scaler : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.DOScale(0, 1);
    }


}
