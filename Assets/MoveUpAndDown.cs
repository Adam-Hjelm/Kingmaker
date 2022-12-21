using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveUpAndDown : MonoBehaviour
{
    public float moveRange = 1;
    public float duration = 1;
    public bool snapping = false;


    // Update is called once per frame
    void Start()
    {
        //GetComponent<RectTransform>().DOPivotY(1f, duration).SetLoops(-1, LoopType.Yoyo);
        transform.DOLocalMoveY(moveRange, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
