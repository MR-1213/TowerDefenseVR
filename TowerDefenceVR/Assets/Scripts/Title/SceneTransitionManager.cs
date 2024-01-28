using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance {get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SceneTransition(string sceneName)
    {
        FadeManager.fadeManagerInstance.FadeOutToIn(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void DelaySceneTransition()
    {
        DOVirtual.DelayedCall(12.0f, () => SceneManager.LoadScene("Title"));
    }
}
