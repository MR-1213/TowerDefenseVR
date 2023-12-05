using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// プレイヤーに対して表示されるUIを管理するクラス
/// </summary>
public class PlayerUIManager : MonoBehaviour
{
    [Header("UI共通")]
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private LineRenderer lineRenderer;
    private int laserAccessCount = 1;

    [Header("魔法UI関係")]
    [SerializeField] private GameObject generatedMagicCanvas;
    private int latestSelectedMagicIndex = 0;
    

    private void Start() 
    {
        LaserPointerDisable();
    }

    public void LaserPointerEnable()
    {
        laserAccessCount++;
        lineRenderer.gameObject.SetActive(true);
    }

    public void LaserPointerDisable()
    {
        laserAccessCount--;
        if(laserAccessCount <= 0)
        {
            lineRenderer.gameObject.SetActive(false);
        }
        
    }

    public void ChangeGeneratedMagicCanvasEnable()
    {
        generatedMagicCanvas.transform.GetChild(latestSelectedMagicIndex).gameObject.SetActive(true);
    }

    public void ChangeGeneratedMagicCanvasDisable()
    {
        int childCount = generatedMagicCanvas.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            if(generatedMagicCanvas.transform.GetChild(i).gameObject.activeSelf)
            {
                generatedMagicCanvas.transform.GetChild(i).gameObject.SetActive(false);
                latestSelectedMagicIndex = i;
                break;
            }
        }
    }

    public void NextMagic()
    {
        int childCount = generatedMagicCanvas.transform.childCount;

        for(int i = 0; i < childCount; i++)
        {
            if(generatedMagicCanvas.transform.GetChild(i).gameObject.activeSelf)
            {
                generatedMagicCanvas.transform.GetChild(i).gameObject.SetActive(false);
                if(i == childCount - 1)
                {
                    generatedMagicCanvas.transform.GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    generatedMagicCanvas.transform.GetChild(i + 1).gameObject.SetActive(true);
                }
                
                break;
            }
        }
        
    }
    
}
