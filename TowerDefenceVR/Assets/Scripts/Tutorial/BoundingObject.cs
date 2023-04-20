using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoundingObject : MonoBehaviour
{
    private bool isBounding = false;

    public void Bound() 
    {
        if(!isBounding)
        {
            isBounding = true;
            transform.DOLocalMoveY(1.0f, 0.7f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).SetLink(gameObject).SetRelative();
        }
        else
        {
            isBounding = false;
            Destroy(gameObject);
        }
        
    }
}
