using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// チュートリアルのタイムライン進行を管理するクラス
/// </summary>
public class TutorialManager : MonoBehaviour
{
    //[SerializeField] private Button generateButton;
    //[SerializeField] private TriggerManager triggerManager;
    private PlayableDirector playableDirector;
    //private float touchStickTime = 0.0f;
    //private int touchStickCount = 0;
    //private float rightStickThreshold = 5.0f;
    //private int leftStickThreshold = 5;

    public bool OKButtonClicked { private get; set; } = false;
    public bool MovedTrigger { private get; set; } = false;
    public bool GrabbedTrigger { get; set; } = false;
    public bool SwordAttackedTrigger { get; set; } = false;
    public bool MagicAttackedTrigger { get; set; } = false;
    public bool KilledTrigger { get; set; } = false;


    //HalfEnemyKilledのコルーチンが開始したことを示すフラグ
    public bool HalfEnemyKilledCoroutineStarted { get; private set; } = false;

    //AllEnemyKilledのコルーチンが開始したことを示すフラグ
    public bool AllEnemyKilledCoroutineStarted { get; private set; } = false;
    private bool isTutorialPause = false;

    public bool IsTutorialFlag { get; private set; }


    public bool ClickedTrigger { get; set; } = false;
    public bool IsEndOfCastingVoice { get; set; } = false;
    public bool GeneratedTrigger { get; set; } = true;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void PauseTimeline(int stateNum)
    {
        PauseTimeline();
        isTutorialPause = true;
        switch (stateNum)
        {
            case 0:
                StartCoroutine(HowToMove());
                break;
            case 1:
                StartCoroutine(WaitPlayer());
                break;
            case 2:
                StartCoroutine(HowToGrabWeapons());
                break;
            case 3:
                StartCoroutine(TryAttackWithSword());
                break;
            case 4:
                StartCoroutine(TryAttackWithMagic());
                break;
            case 5:
                StartCoroutine(HalfEnemyKilled());
                break;
            case 6:
                StartCoroutine(AllEnemyKilled());
                break;
        }
    }

    public void PauseTimeline()
    {
        playableDirector.Pause();
    }

    public void ResumeTimeline()
    {
        if(!isTutorialPause) playableDirector.Resume();
    }

    private IEnumerator HowToMove()
    {
        yield return new WaitUntil(() => OKButtonClicked);
        OKButtonClicked = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator WaitPlayer()
    {
        yield return new WaitUntil(() => MovedTrigger);
        MovedTrigger = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator HowToGrabWeapons()
    {
        GrabbedTrigger = false;
        yield return new WaitUntil(() => GrabbedTrigger);
        GrabbedTrigger = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator TryAttackWithSword()
    {
        SwordAttackedTrigger = false;
        yield return new WaitUntil(() => SwordAttackedTrigger);
        SwordAttackedTrigger = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    public IEnumerator TryAttackWithMagic()
    {
        MagicAttackedTrigger = false;
        yield return new WaitUntil(() => MagicAttackedTrigger);
        MagicAttackedTrigger = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator HalfEnemyKilled()
    {
        HalfEnemyKilledCoroutineStarted = true;
        KilledTrigger = false;
        yield return new WaitUntil(() => KilledTrigger);
        KilledTrigger = false;
        HalfEnemyKilledCoroutineStarted = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator AllEnemyKilled()
    {
        AllEnemyKilledCoroutineStarted = true;
        KilledTrigger = false;
        yield return new WaitUntil(() => KilledTrigger);
        KilledTrigger = false;
        AllEnemyKilledCoroutineStarted = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

}
