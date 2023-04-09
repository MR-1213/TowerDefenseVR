using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TMP_Text generateText;
    [SerializeField] private Chat chat;
    [SerializeField] private GameObject playerMagicsCanvas;
    [SerializeField] private GameObject playerNewMagicCanvas;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private TMP_InputField magicInputField;
    [SerializeField] private LineRenderer lineRenderer;
    private bool canvasEnable = false;
    private TouchScreenKeyboard keyboard;

    private void Awake() 
    {
        playerMagicsCanvas.SetActive(canvasEnable);
        playerNewMagicCanvas.SetActive(canvasEnable);
        lineRenderer.enabled = false;
    }

    private void Update() 
    {
        //Xボタンを押したら魔法生成UIを表示・非表示を切り替える
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            canvasEnable = !canvasEnable;
            playerMagicsCanvas.SetActive(canvasEnable);
            lineRenderer.enabled = canvasEnable;
            if(mainCamera != null)
            {
                playerMagicsCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.5f;
                playerNewMagicCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.5f - mainCamera.transform.up * 1.0f;

                Vector3 lookPos1 = mainCamera.transform.position - playerMagicsCanvas.transform.position;
                Vector3 lookPos2 = mainCamera.transform.position - playerNewMagicCanvas.transform.position;

                playerMagicsCanvas.transform.rotation = Quaternion.LookRotation(lookPos1);
                playerMagicsCanvas.transform.Rotate(Vector3.up, 180f, Space.Self);
                playerNewMagicCanvas.transform.rotation = Quaternion.LookRotation(lookPos2);
                playerNewMagicCanvas.transform.Rotate(Vector3.up, 180f, Space.Self);
            }
        }

        if(keyboard != null)
        {
            magicInputField.text = keyboard.text;
        }
    }

    public void OnNewMagicButton()
    {
        playerMagicsCanvas.SetActive(false);
        playerNewMagicCanvas.SetActive(true);

        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void OnGenerateButton()
    {
        chat.OnGenerateButtonClick();
    }

    public void OnBackButton()
    {
        playerNewMagicCanvas.SetActive(false);
        playerMagicsCanvas.SetActive(true);
    }

    public void OnEndButton()
    {
        playerMagicsCanvas.SetActive(false);
        playerNewMagicCanvas.SetActive(false);
        canvasEnable = false;
        lineRenderer.enabled = false;
    }
}
