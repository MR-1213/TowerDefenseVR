using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SearchMagicInformation
{
    private const string ATTRIBUTE = "属性:";
    private const string POWER = "威力:";
    private const string ATTRIBUTE_END = ":attributeEnd";
    private const string POWER_END = ":powerEnd";
    private string newMagic; //ChatGPTからの返答を格納
    private string magicInfoKey = string.Empty; //属性と威力を結合した文字列

    public SearchMagicInformation(string newMagic)
    {
        //前後の空白を削除し、<と>を削除した後、newMagicに格納
        string trimedText = newMagic.Trim().Replace("<", "").Replace(">", "");
        this.newMagic = trimedText;
    }

    public string GetMagicInfo()
    {
        //newMagicがNullまたは空文字の場合は終了
        if (string.IsNullOrEmpty(newMagic)) return magicInfoKey;

        //「属性」の表記がある場所を探し、その後ろにある「:attributeEnd」の表記がある場所を探す
        int startAttributeIndex = newMagic.IndexOf(ATTRIBUTE);
        int endAttributeIndex = newMagic.IndexOf(ATTRIBUTE_END);
        //「属性」が見つからなかった場合は終了
        if(startAttributeIndex < 0)
        {
            return magicInfoKey;
        }

        //「威力」の表記がある場所を探し、その後ろにある「:powerEnd」の表記がある場所を探す
        int startPowerIndex = newMagic.IndexOf(POWER);
        int endPowerIndex = newMagic.IndexOf(POWER_END);
        //「威力」が見つからなかった場合は終了
        if(startPowerIndex < 0)
        {
            return magicInfoKey;
        }

        //「属性」の表記がある場所から「:attributeEnd」の表記がある場所までの文字列を取得
        string attributeInfo = newMagic.Substring(startAttributeIndex + ATTRIBUTE.Length, endAttributeIndex - startAttributeIndex - ATTRIBUTE.Length);
        //「威力」の表記がある場所から「:powerEnd」の表記がある場所までの文字列を取得
        string powerInfo = newMagic.Substring(startPowerIndex + POWER.Length, endPowerIndex - startPowerIndex - POWER.Length);

        //属性と威力を結合した文字列を返す
        return SetMagicInfo(attributeInfo, powerInfo);
    }

    private string SetMagicInfo(string attributeInfo, string powerInfo)
    {
        //威力を0~100の10の位でグループ分け
        int powerGroup = int.Parse(powerInfo) /10 * 10;
        //属性と威力を結合した文字列を返す
        magicInfoKey = attributeInfo + "-" + string.Format("{0:000}", powerGroup);
        return magicInfoKey;
    }
}
