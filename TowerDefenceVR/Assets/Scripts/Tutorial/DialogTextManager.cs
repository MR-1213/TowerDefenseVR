using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// チュートリアル中、音声テキストを表示する際に使うクラス
/// </summary>
public class DialogTextManager : MonoBehaviour
{
    /// <summary>
    /// 文字送り演出を表示する
    /// </summary>
    public void Show(TMP_Text explanationText)
    {
        //テキストが空、もしくは音声が空なら何もしない
        if (explanationText.text == "")
        {
            Debug.Log("テキストが空です");
            return;
        }

        //テキスト送りを開始する
        StartCoroutine(ShowText(explanationText));
    }

    private IEnumerator ShowText(TMP_Text explanationText)
    {
        // テキスト全体の長さ
        var textLength = explanationText.text.Length;
        
        // １文字ずつ表示する演出
        for (var i = 0; i < textLength; i++)
        {
            // 徐々に表示文字数を増やしていく
            explanationText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.1f);
        }

        // 演出が終わったら全ての文字を表示する
        explanationText.maxVisibleCharacters = textLength;
    }
}
