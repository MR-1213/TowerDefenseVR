using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Microsoft.Unity.VisualStudio.Editor;

/// <summary>
/// 魔法を生成/管理するクラス
/// </summary>
public class GenerateMagic : MonoBehaviour
{
    public static GenerateMagic Instance { get; private set; }

    [SerializeField] Transform magicCastParent;
    [SerializeField] Transform generatedMagicUIParent;
    [SerializeField] GameObject generatedMagicUI;

    [SerializeField, Tooltip("0:火, 1:水, 2:氷, 3:風, 4:雷, 5:土, 6:光, 7:闇")] public GameObject[] playerMagics = new GameObject[8];
    [SerializeField, Tooltip("0:火, 1:水, 2:氷, 3:風, 4:雷, 5:土, 6:光, 7:闇")] public GameObject[] enemyMagics = new GameObject[8];

    private string[] magicNames = new string[8] {"火", "水", "氷", "風", "雷", "土", "光", "闇"};

    private Dictionary<string, GameObject> magicDictionary = new Dictionary<string, GameObject>();

    private void Start()
    {
        for(int i = 0; i < playerMagics.Length; i++)
        {
            magicDictionary.Add(magicNames[i], playerMagics[i]);
        }
    }

    public void GenerateSelectedMagic(GameObject selectedMagic)
    {
        string magicName = selectedMagic.GetComponentInChildren<TextMeshProUGUI>().text;
        if(magicDictionary.ContainsKey(magicName))
        {
            Instantiate(magicDictionary[magicName], magicCastParent.position, magicCastParent.rotation, magicCastParent);
        }
        
    }
}
