using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Banzan.Lib.Utility;

/// <summary>
/// プレイヤーに対して表示されるUIを管理するクラス
/// </summary>
public class PlayerUIManager : MonoBehaviour
{
    [Header("参照スクリプト")]
    [SerializeField] private Chat chat;

    [Header("UI共通")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("魔法生成UI関係")]
    [SerializeField] private GameObject playerNewMagicCanvas; 
    [SerializeField] private TMP_InputField magicInputField;
    private bool newMagicCanvasEnable = false;
    private TouchScreenKeyboard keyboard;

    [Header("生成済み魔法UI関係")]
    [SerializeField] private TMP_Dropdown generatedMagicDropdown;
    private bool generatedMagicCanvasEnable = false;

    private void Awake() 
    {
        playerNewMagicCanvas.SetActive(newMagicCanvasEnable);
        lineRenderer.enabled = false;
    }

    private void Update() 
    {
        //キーボードの入力をインプットフィールドに反映する
        if(keyboard != null)
        {
            magicInputField.text = keyboard.text;
        }

    }

    public void ChangeNewMagicCanvasEnable()
    {
        //魔法生成のUIの表示・非表示を切り替える
        newMagicCanvasEnable = !newMagicCanvasEnable;
        playerNewMagicCanvas.SetActive(newMagicCanvasEnable);

        //レーザーポインターの表示・非表示も切り替える
        lineRenderer.enabled = newMagicCanvasEnable;
        
        if(mainCamera != null)
        {
            //カメラの向いている方向の少し下にUIを配置する
            playerNewMagicCanvas.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 1.5f - mainCamera.transform.up * 1.0f;

            Vector3 lookPos2 = mainCamera.transform.position - playerNewMagicCanvas.transform.position;

            playerNewMagicCanvas.transform.rotation = Quaternion.LookRotation(lookPos2);
            playerNewMagicCanvas.transform.Rotate(Vector3.up, 180f, Space.Self);
        }
    }

    public void OnGenerateButton()
    {
        chat.OnGenerateButtonClick();
    }

    public void OnBackButton()
    {
        playerNewMagicCanvas.SetActive(false);
        newMagicCanvasEnable = false;
        lineRenderer.enabled = false;
    }

    public void ChangeGeneratedMagicCanvasEnable()
    {
        generatedMagicCanvasEnable = !generatedMagicCanvasEnable;
        lineRenderer.enabled = generatedMagicCanvasEnable;
    }
    
}
