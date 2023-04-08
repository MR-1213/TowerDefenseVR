using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SearchMagicInformation
{
    private const string ATTRIBUTE = "属性:";
    private const string POWER = "威力:"; 
    private string newMagic;
    private string magicInfoKey;

    public SearchMagicInformation(string newMagic)
    {
        string trimedText = newMagic.Trim();
        this.newMagic = trimedText;
    }

    public string GetMagicInfo()
    {
        if (string.IsNullOrEmpty(newMagic)) return magicInfoKey;
        //「属性」の表記がある場所を探す
        int startAttributeIndex = newMagic.IndexOf(ATTRIBUTE);
        int endAttributeIndex = newMagic.IndexOf(":attributeEnd");
        if(startAttributeIndex < 0)
        {
            return magicInfoKey;
        }

        int startPowerIndex = newMagic.IndexOf(POWER);
        int endPowerIndex = newMagic.IndexOf(":powerEnd");
        if(startPowerIndex < 0)
        {
            return magicInfoKey;
        }

        string attributeInfo = newMagic.Substring(startAttributeIndex + ATTRIBUTE.Length, endAttributeIndex - startAttributeIndex - ATTRIBUTE.Length);
        string powerInfo = newMagic.Substring(startPowerIndex + POWER.Length, endPowerIndex - startPowerIndex - POWER.Length);

        return SetMagicInfo(attributeInfo, powerInfo);
    }

    private string SetMagicInfo(string attributeInfo, string powerInfo)
    {
        int powerGroup = int.Parse(powerInfo) /10 * 10;

        magicInfoKey = attributeInfo + "-" + string.Format("{0:000}", powerGroup);
        return magicInfoKey;
    }
}
