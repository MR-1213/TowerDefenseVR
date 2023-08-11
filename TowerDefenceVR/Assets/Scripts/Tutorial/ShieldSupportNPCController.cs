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
            tutorialManager.AttackedTrigger = true;
        }
    }

    public void StartLookAtPlayer()
    {
        shieldTransform.position = new Vector3(-0.003981794f, 0.1466343f, -0.0564862f);
        shieldTransform.rotation = Quaternion.Euler(0f, 11.443f, -90.0f);
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

            transform.LookAt(playerPos);
            yield return null;
        }
    }

    public void StopLookAtPlayer()
    {
        lookAtPlayerTrigger = false;
        shieldTransform.position = new Vector3(-0.004f, 0f, -0.075f);
        shieldTransform.rotation = Quaternion.Euler(new Vector3(0f, 93.0f, -90.0f));
    }
}
