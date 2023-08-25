using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager sceneManagerInstance;

    private void Awake()
    {
        if (sceneManagerInstance == null)
        {
            sceneManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SceneTransition()
    {
        FadeManager.fadeManagerInstance.FadeOutToIn(() =>
        {
            SceneManager.LoadScene("Tutorial");
        });
    }

    public void DelaySceneTransition()
    {
        DOVirtual.DelayedCall(12.0f, () => SceneManager.LoadScene("Title"));
    }
}
