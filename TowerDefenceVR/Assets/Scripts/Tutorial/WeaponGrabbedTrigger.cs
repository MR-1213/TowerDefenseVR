using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrabbedTrigger : MonoBehaviour
{
    public AudioClip dropSwordSE;

    private TutorialManager tutorialManager;
    private PlayerWeaponManager playerWeaponManager;
    private PlayerControllerManager playerControllerManager;
    private float grabbingTime = 0f;

    private void Start()
    {
        OVRGrabbable parent = transform.parent.GetComponent<OVRGrabbable>();
        tutorialManager = GameObject.Find("TutorialExplanationManager").GetComponent<TutorialManager>();
        playerWeaponManager = GameObject.Find("Player").GetComponent<PlayerWeaponManager>();
        playerControllerManager = GameObject.Find("Player").GetComponent<PlayerControllerManager>();
        parent.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHand") && gameObject.CompareTag("PlayerSword"))
        {
            playerWeaponManager.AddWeapon(gameObject.transform.parent.gameObject);
            playerControllerManager.isGrabWeapon = true;
            StartCoroutine(CountGrabbingSwordTime());
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            var audio = this.gameObject.GetComponentInParent<AudioSource>();
            audio.clip = dropSwordSE;
            audio.PlayOneShot(audio.clip);

            playerControllerManager.isGrabWeapon = false;
            Destroy(this.transform.parent.gameObject, 2.0f);
        }
    }

    private IEnumerator CountGrabbingSwordTime()
    {
        grabbingTime = 0f;
        while(true)
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                grabbingTime += Time.deltaTime;
            }
            else if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                playerControllerManager.isGrabWeapon = false;
                yield break;
            }

            if(grabbingTime > 2.0f)
            {
                tutorialManager.GrabbedTrigger = true;

                playerControllerManager.grabWeapon = gameObject.transform.parent.gameObject;

                break;
            }

            yield return null;
        }
    }
}
