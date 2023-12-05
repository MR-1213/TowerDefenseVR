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
    private PlayableDirector playableDirector;

    public bool OKButtonClicked { private get; set; } = false;
    public bool MovedTrigger { private get; set; } = false;
    public bool GrabbedTrigger { get; set; } = false;
    public bool SwordAttackedTrigger { get; set; } = false;
    public bool MagicAttackedTrigger { get; set; } = true;
    public bool KilledTrigger { get; set; } = false;
    public bool PassingTrigger { get; set; } = false;


    //HalfEnemyKilledのコルーチンが開始したことを示すフラグ
    public bool PTPCoroutineStarted { get; private set; } = false;

    //AllEnemyKilledのコルーチンが開始したことを示すフラグ
    public bool AllCoroutineStarted { get; private set; } = false;

    //AdditionalEnemyKilledのコルーチンが開始したことを示すフラグ
    public bool AdditionalCoroutineStarted { get; private set; } = false;


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
                StartCoroutine(PassingTransitPoint());
                break;
            case 6:
                StartCoroutine(AllEnemyKilled());
                break;
            case 7:
                StartCoroutine(AdditionalEnemyKilled());
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

    private IEnumerator PassingTransitPoint()
    {
        PTPCoroutineStarted = true;
        PassingTrigger = false;
        yield return new WaitUntil(() => PassingTrigger);
        PassingTrigger = false;
        PTPCoroutineStarted = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator AllEnemyKilled()
    {
        AllCoroutineStarted = true;
        KilledTrigger = false;
        yield return new WaitUntil(() => KilledTrigger);
        KilledTrigger = false;
        AllCoroutineStarted = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

    private IEnumerator AdditionalEnemyKilled()
    {
        AdditionalCoroutineStarted = true;
        KilledTrigger = false;
        yield return new WaitUntil(() => KilledTrigger);
        KilledTrigger = false;
        AdditionalCoroutineStarted = false;

        isTutorialPause = false;
        ResumeTimeline();
    }

}
