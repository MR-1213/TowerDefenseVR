using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSupportNPCController : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private Transform playerPos;
    [SerializeField] private Transform shieldTransform;
    private bool lookAtPlayerTrigger = true;

    private void OnAnimatorMove()
    {
        transform.position = GetComponent<Animator>().rootPosition;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("PlayerSword"))
        {
            tutorialManager.SwordAttackedTrigger = true;
        }

        if(other.gameObject.CompareTag("PlayerMagic"))
        {
            tutorialManager.MagicAttackedTrigger = true;
        }
    }

    public void StartLookAtPlayer()
    {
        StartCoroutine(LookAtPlayer());
    }
    
    private IEnumerator LookAtPlayer()
    {
        while(true)
        {
            if(!lookAtPlayerTrigger)
            {
                yield break;
            }

            Vector3 playerDirection = new Vector3(playerPos.position.x, 0f, playerPos.position.z);
            transform.LookAt(playerDirection);
            yield return null;
        }
    }

    public void StopLookAtPlayer()
    {
        lookAtPlayerTrigger = false;
    }
}
