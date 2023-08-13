using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Banzan.Lib.Utility;

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

    public bool OKButtonClicked {private get; set; } = false;
    public bool MovedTrigger {private get; set; } = false;
    public bool GrabbedTrigger { get; set; } = false;
    public bool SwordAttackedTrigger { get; set; } = false;
    public bool MagicAttackedTrigger { get; set; } = false;

    public bool IsTutorialFlag { get; private set;}
    
    
    public bool ClickedTrigger { get; set; } = false;
    public bool IsEndOfCastingVoice { get; set; } = false;
    public bool GeneratedTrigger { get; set; } = true;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void PauseTimeline(int stateNum)
    {
        playableDirector.Pause();
        switch(stateNum)
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
        }
    }

    private void ResumeTimeline()
    {
        playableDirector.Resume();
    }

    private IEnumerator HowToMove()
    {
        yield return new WaitUntil(() => OKButtonClicked);
        OKButtonClicked = false;

        ResumeTimeline();
    }

    private IEnumerator WaitPlayer()
    {
        yield return new WaitUntil(() => MovedTrigger);
        MovedTrigger = false;

        ResumeTimeline();
    }

    private IEnumerator HowToGrabWeapons()
    {
        GrabbedTrigger = false;
        yield return new WaitUntil(() => GrabbedTrigger);
        GrabbedTrigger = false;

        ResumeTimeline();
    }

    private IEnumerator TryAttackWithSword()
    {
        SwordAttackedTrigger = false;
        yield return new WaitUntil(() => SwordAttackedTrigger);
        SwordAttackedTrigger = false;

        ResumeTimeline();
    }

    public IEnumerator TryAttackWithMagic()
    {
        MagicAttackedTrigger = false;
        yield return new WaitUntil(() => MagicAttackedTrigger);
        MagicAttackedTrigger = false;

        ResumeTimeline();
    }

    /*
    public void IsTutorial()
    {
        IsTutorialFlag = true;
    }

    IEnumerator WaitAndResumeForRightThick()
    {
        touchStickTime = 0.0f;
        while(true)
        {
            if( OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
            {
                touchStickTime += Time.deltaTime;
            }

            if(touchStickTime > rightStickThreshold)
            {
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator WaitAndResumeForLeftThick()
    {
        touchStickCount = 0;
        while(true)
        {
            if(OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch) ||
                OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch))
            {
                touchStickCount++;
            }

            if(touchStickCount > leftStickThreshold)
            {
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator MovedToSwordPoint()
    {
        while(true)
        {
            if(MovedTrigger)
            {
                MovedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator GrabbedSword()
    {
        GrabbedTrigger = false;
        triggerManager.isInProcess = false;
        while(true)
        {
            if(GrabbedTrigger)
            {
                GrabbedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator MovedToEnemyPoint()
    {
        MovedTrigger = false;
        while(true)
        {
            if(MovedTrigger)
            {
                MovedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator KilledEnemy()
    {
        while(true)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator ClickedYButton()
    {
        while(true)
        {
            if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
            {
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator ClickedGenerateButton()
    {
        generateButton.interactable = true;
        ClickedTrigger = false;
        while(true)
        {
            if(ClickedTrigger)
            {
                ClickedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator GeneratedMagic()
    {
        GeneratedTrigger = false;
        while(true)
        {
            if(GeneratedTrigger)
            {
                GeneratedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator KilledMultiEnemies()
    {
        while(true)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }

    IEnumerator GettingCloseMultipleEnemies()
    {
        MovedTrigger = false;
        while(true)
        {
            if(MovedTrigger)
            {
                MovedTrigger = false;
                break;
            }

            yield return null;
        }

        ResumeTimeline();
    }
    */
}
