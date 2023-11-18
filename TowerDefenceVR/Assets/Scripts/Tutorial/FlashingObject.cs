using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

/// <summary>
/// チュートリアル中、オブジェクトを点滅させる際に使うクラス
/// </summary>
public class FlashingObject : MonoBehaviour
{
    private Material flashingMaterial;
    private bool isFlashing = false;
    private void Start() 
    {
        flashingMaterial = GetComponent<Renderer>().material;

        if(SceneManager.GetActiveScene().name == "Title")
        {
            Flash();
        }
    }
    
    public void Flash()
    {
        if(!isFlashing)
        {
            isFlashing = true;
            flashingMaterial.DOFade(0.5f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }
        else
        {
            isFlashing = false;
            //点滅を止める
            flashingMaterial.DOKill();
            //マテリアルの透明度を0にする
            flashingMaterial.DOFade(0.0f, 0.01f);
        }
    }

    public void StopFlashing()
    {
        isFlashing = false;
        //点滅を止める
        flashingMaterial.DOKill();
        //マテリアルの透明度を0にする
        flashingMaterial.DOFade(0.0f, 0.01f);
    }

    public void ReStartFlashing()
    {
        isFlashing = true;
        flashingMaterial.DOFade(0.5f, 1.2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
