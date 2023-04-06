using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateMagic : MonoBehaviour
{
    private const string ATTRIBUTE = "属性:";
    private const string POWER = "威力:"; 
    private string newMagic;

    private string attributeInfo;
    private string powerInfo;

    public GenerateMagic(string newMagic)
    {
        string trimedText = newMagic.Trim();
        this.newMagic = trimedText;
    }

    public string GetMagicInfo()
    {
        if (string.IsNullOrEmpty(newMagic)) return "入力なし";
        //「属性」の表記がある場所を探す
        int attributeIndex = newMagic.IndexOf(ATTRIBUTE);
        if(attributeIndex < 0)
        {
            return "Attributeでエラー";
        }

        int powerIndex = newMagic.IndexOf(POWER);
        if(powerIndex < 0)
        {
            return "POWERでエラー";
        }

        attributeInfo = newMagic.Substring(attributeIndex + ATTRIBUTE.Length, 1);
        powerInfo = newMagic.Substring(powerIndex + POWER.Length, 3);

        return SetMagic(attributeInfo, powerInfo);
    }

    private string SetMagic(string attributeInfo, string powerInfo)
    {
        switch(attributeInfo)
        {
            case "火":
                return "火魔法";
            case "水":
                return "水魔法";
            case "風":
                return "風魔法";
            case "氷":
                return "氷魔法";
            case "雷":
                return "雷魔法";
            case "土":
                return "土魔法";
            case "光":
                return "光魔法";
            case "闇":
                return "闇魔法";
            default:
                return "エラー";    
        }
    }

    public string GetInfo()
    {
        return ":" + attributeInfo + ":" + powerInfo + ":";
    }
}
