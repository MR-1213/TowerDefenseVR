using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    private PlayableDirector playableDirector;
    private float touchStickTime = 0.0f;
    private float rightStickThreshold = 8.0f;
    private float leftStickThreshold = 5.0f;

    public bool MovedTrigger { get; set; } = false;
    public bool GrabbedTrigger { get; set; } = false;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    public void PauseTimeline(string targetName)
    {
        playableDirector.Pause();
        switch(targetName)
        {
            case "RightThumbstick":
                StartCoroutine(WaitAndResumeForRightThick());
                Debug.Log("RightThumbstick");
                break;
            case "LeftThumbstick":
                StartCoroutine(WaitAndResumeForLeftThick());
                Debug.Log("LeftThumbstick");
                break;
            case "MoveToSwordPoint":
                StartCoroutine(MovedToSwordPoint());
                break;
            case "GrabbedSword":
                StartCoroutine(GrabbedSword());
                break;
        }
    }

    public void ResumeTimeline()
    {
        playableDirector.Resume();
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
        touchStickTime = 0.0f;
        while(true)
        {
            if( OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.LTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.LTouch) ||
                OVRInput.Get(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch))
            {
                touchStickTime += Time.deltaTime;
            }

            if(touchStickTime > leftStickThreshold)
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
}
