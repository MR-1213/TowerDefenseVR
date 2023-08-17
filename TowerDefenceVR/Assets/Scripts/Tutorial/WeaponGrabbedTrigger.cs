using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGrabbedTrigger : MonoBehaviour
{
    public AudioClip dropSwordSE;

    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private PlayerWeaponManager playerWeaponManager;
    [SerializeField] private PlayerControllerManager playerControllerManager;
    private float grabbingTime = 0f;

    private void Start()
    {
        OVRGrabbable parent = transform.parent.GetComponent<OVRGrabbable>();
        parent.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerHand") && gameObject.CompareTag("PlayerSword"))
        {
            playerWeaponManager.AddWeapon(gameObject.transform.parent.gameObject);
            StartCoroutine(CountGrabbingSwordTime());
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            var audio = this.gameObject.GetComponentInParent<AudioSource>();
            audio.clip = dropSwordSE;
            audio.PlayOneShot(audio.clip);
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
                yield break;
            }

            if(grabbingTime > 2.0f)
            {
                tutorialManager.GrabbedTrigger = true;

                playerControllerManager.isGrabWeapon = true;
                playerControllerManager.grabWeapon = gameObject.transform.parent.gameObject;

                break;
            }

            yield return null;
        }
    }
}
