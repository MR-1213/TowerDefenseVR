using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShieldSupportNPCController : MonoBehaviour
{
    public AudioClip[] swordSlashSE;

    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private Transform playerPos;

    private Slider shieldHPSlider;
    private bool lookAtPlayerTrigger = true;

    private void OnAnimatorMove()
    {
        transform.position = GetComponent<Animator>().rootPosition;
    }
    
    private void Start() 
    {
        shieldHPSlider = GetComponentInChildren<Slider>();
        shieldHPSlider.minValue = 0f;
        shieldHPSlider.maxValue = 60.0f;
        shieldHPSlider.value = shieldHPSlider.maxValue;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("PlayerSword"))
        {
            var audio = other.gameObject.GetComponentInParent<AudioSource>();
            audio.clip = swordSlashSE[Random.Range(0, swordSlashSE.Length)];
            audio.PlayOneShot(audio.clip);

            if(shieldHPSlider.value <= 30.0f)
            {
                shieldHPSlider.value = 30.0f;
            }
            else
            {
                shieldHPSlider.DOValue(shieldHPSlider.value - 10.0f, 0.5f)
                .OnComplete(() => {
                    if(shieldHPSlider.value <= 30.0f)
                    {
                        tutorialManager.SwordAttackedTrigger = true;
                    }
                });
            }
            
        }

        if(other.gameObject.CompareTag("PlayerMagic"))
        {
            if(shieldHPSlider.value > 30.0f)
            {
                shieldHPSlider.value = 60.0f;
            }
            else
            {
                shieldHPSlider.DOValue(shieldHPSlider.value - 10.0f, 0.5f)
                .OnComplete(() => {
                    if(shieldHPSlider.value <= 0f)
                    {
                        tutorialManager.MagicAttackedTrigger = true;
                    }
                });
            }
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
