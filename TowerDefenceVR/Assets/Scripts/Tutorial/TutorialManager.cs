using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialManager : MonoBehaviour
{
    private PlayableDirector playableDirector;
    float touchStickTime = 0.0f;
    private float rightStickThreshold = 10.0f;
    private float leftStickThreshold = 5.0f;

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
                break;
            case "LeftThumbstick":
                StartCoroutine(WaitAndResumeForLeftThick());
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
}
