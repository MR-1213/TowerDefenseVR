using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationHistoryManager : MonoBehaviour
{
    public GameObject conversationHistory;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private OVRScreenFade screenFade;
    [SerializeField] private Transform playerPos;
    [SerializeField] private GameObject settingCanvas;
    [SerializeField] private GameObject conversationCanvas;
    [SerializeField] private GameObject commonButtonCanvas;
    [SerializeField] private GameObject voiceTextCanvas;
    [SerializeField] private GameObject content;

    private bool isOpenSetting = false;

    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.RawButton.Start) && !isOpenSetting)
        {
            isOpenSetting = true;
            screenFade.fadeTime = 0f;
            //screenFade.fadeWaitTime = 1.0f;
            screenFade.FadeOut();

            StartCoroutine(SettingPanelActive());
        }
        else if(OVRInput.GetDown(OVRInput.RawButton.Start) && isOpenSetting)
        {
            isOpenSetting = false;
            screenFade.FadeIn();

            StartCoroutine(SettingPanelInactive());
        }
    }

    private IEnumerator SettingPanelActive()
    {
        yield return new WaitForSeconds(1.0f);
        settingCanvas.SetActive(true);
        transform.position = playerPos.position + playerPos.forward * 2.0f;
        transform.rotation = Quaternion.LookRotation(playerPos.forward);

        voiceTextCanvas.layer = LayerMask.NameToLayer("UI");
        playerUIManager.LaserPointerEnable();
        tutorialManager.PauseTimeline();
    }

    private IEnumerator SettingPanelInactive()
    {
        yield return new WaitForSeconds(1.0f);
        settingCanvas.SetActive(false);
        conversationCanvas.SetActive(false);
        commonButtonCanvas.SetActive(false);

        voiceTextCanvas.layer = LayerMask.NameToLayer("Transparent UI");
        playerUIManager.LaserPointerDisable();
        tutorialManager.ResumeTimeline();
    }

    public void AddConversationHistory(string text)
    {
        var history = Instantiate(conversationHistory, content.transform);
        history.GetComponentInChildren<TMP_Text>().text = text;
    }
}
