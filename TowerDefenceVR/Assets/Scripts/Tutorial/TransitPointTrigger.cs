using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitPointTrigger : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(tutorialManager.PTPCoroutineStarted)
            {
                tutorialManager.PassingTrigger = true;
            }
        }
    }
}
