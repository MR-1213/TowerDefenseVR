using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatSupportNPCController : MonoBehaviour
{
    public AudioClip[] swordSlashSE;
    private Combat_IdleCallback combatCallback;
    private Animator animator;
    private AudioSource audioSource;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        combatCallback = animator.GetBehaviour<Combat_IdleCallback>();
        StartCoroutine(ChangeAnimState());
    }

    private IEnumerator ChangeAnimState()
    {
        while(true)
        {
            if(combatCallback.isEnterIdle)
            {
                combatCallback.isEnterIdle = false;
                //0f~5fのランダムな値
                float randomTime = Random.Range(0f, 5f);
                yield return new WaitForSeconds(randomTime);
                animator.SetTrigger("ChangeAnimState");
                audioSource.PlayOneShot(swordSlashSE[Random.Range(0, swordSlashSE.Length)]);
            }

            yield return new WaitForSeconds(0.2f);
            
        }
    }

    public void DieAnimState()
    {
        animator.SetTrigger("DieTrigger");
        StopAllCoroutines();
    }

    public void IdleAnimState()
    {
        animator.SetTrigger("IdleTrigger");
        StopAllCoroutines();
    }
}
