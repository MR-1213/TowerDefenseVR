using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationHistoryManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private OVRScreenFade screenFade;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject voiceTextCanvas;

    private bool isOpenSetting = false;

    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.RawButton.Start) && !isOpenSetting)
        {
            Debug.Log("メニューを開く");
            isOpenSetting = true;
            screenFade.fadeTime = 0f;
            screenFade.fadeWaitTime = 1.0f;
            screenFade.FadeOut();

            StartCoroutine(SettingPanelActive());
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.Start) && isOpenSetting)
        {
            Debug.Log("メニューを閉じる");
            isOpenSetting = false;
            screenFade.FadeIn();

            StartCoroutine(SettingPanelInactive());
        }
    }

    private IEnumerator SettingPanelActive()
    {
        yield return new WaitForSeconds(1.0f);
        settingPanel.SetActive(true);
        transform.position = playerPos.position + playerPos.forward * 2.0f;
        transform.rotation = Quaternion.LookRotation(playerPos.forward);

        voiceTextCanvas.layer = LayerMask.NameToLayer("UI");
        playerUIManager.LaserPointerEnable();
        tutorialManager.PauseTimeline();
    }

    private IEnumerator SettingPanelInactive()
    {
        yield return new WaitForSeconds(1.0f);
        settingPanel.SetActive(false);

        voiceTextCanvas.layer = LayerMask.NameToLayer("Transparent UI");
        playerUIManager.LaserPointerDisable();
        tutorialManager.ResumeTimeline();
    }
}
