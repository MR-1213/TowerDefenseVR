using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    public static FadeManager fadeManagerInstance;
    private CanvasGroup fadeCanvas;
    private GameObject centerEye;
    private float timer;

    private void Awake()
    {
        if (fadeManagerInstance == null)
        {
            fadeManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        fadeCanvas = GetComponent<CanvasGroup>();
        centerEye = GameObject.Find("CenterEyeAnchor");
    }

    private void Update() 
    {
        timer += Time.deltaTime;

        if(centerEye != null)
        {
            this.transform.position = centerEye.transform.position + centerEye.transform.forward * 0.3f;
            Vector3 lookPos = centerEye.transform.position - this.transform.position;
            this.transform.rotation = Quaternion.LookRotation(lookPos);
        }
        else if(timer < 2.0f)
        {
            centerEye = GameObject.Find("CenterEyeAnchor");
        }
    }

    public void FadeOutToIn(TweenCallback action)
    {
        fadeCanvas.DOFade(1.0f, 2.0f).OnComplete(() =>
        {
            action();
            FadeIn();
        });
    }

    private void FadeIn()
    {
        DOVirtual.DelayedCall(2.0f, () =>
        {
            fadeCanvas.DOFade(0f, 2.0f);
        });
    }
}
