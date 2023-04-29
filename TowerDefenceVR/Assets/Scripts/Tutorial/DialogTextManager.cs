using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTextManager : MonoBehaviour
{
    private TMP_Text explanationText;
    private float delayDuration = 0.1f; // 次の文字を表示するまでの時間[s]
    private Coroutine showCoroutine;

    /// <summary>
    /// 文字送り演出を表示する
    /// </summary>
    public void Show(TMP_Text text)
    {
        //表示するテキストを設定する
        explanationText = text;
        //テキストが空なら何もしない
        if (explanationText.text == "")
        {
            return;
        }

        // 前回の演出処理が走っていたら、停止
        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }

        // １文字ずつ表示する演出のコルーチンを実行する
        showCoroutine = StartCoroutine(ShowCoroutine());
    }

    // １文字ずつ表示する演出のコルーチン
    IEnumerator ShowCoroutine()
    {
        // GC Allocを最小化するためキャッシュしておく
        var delay = new WaitForSeconds(delayDuration);

        // テキスト全体の長さ
        var length = explanationText.text.Length;
        
        // １文字ずつ表示する演出
        for (var i = 0; i < length; i++)
        {
            // 徐々に表示文字数を増やしていく
            explanationText.maxVisibleCharacters = i;
            
            // 一定時間待機
            yield return delay;
        }

        // 演出が終わったら全ての文字を表示する
        explanationText.maxVisibleCharacters = length;

        showCoroutine = null;
    }
}
