using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SearchMagicInformation : MonoBehaviour
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
        int attributeIndex = newMagic.IndexOf(ATTRIBUTE);
        if(attributeIndex < 0)
        {
            return magicInfoKey;
        }

        int powerIndex = newMagic.IndexOf(POWER);
        if(powerIndex < 0)
        {
            return magicInfoKey;
        }

        string attributeInfo = newMagic.Substring(attributeIndex + ATTRIBUTE.Length, 1);
        string powerInfo = newMagic.Substring(powerIndex + POWER.Length, 3);

        return SetMagic(attributeInfo, powerInfo);
    }

    private string SetMagic(string attributeInfo, string powerInfo)
    {
        int powerGroup = int.Parse(powerInfo) /10 * 10;

        magicInfoKey = attributeInfo + "-" + string.Format("%03d", powerGroup);
        return magicInfoKey;
    }
}
