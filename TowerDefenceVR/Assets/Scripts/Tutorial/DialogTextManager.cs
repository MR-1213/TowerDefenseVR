using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// チュートリアル中、音声テキストを表示する際に使うクラス
/// </summary>
public class DialogTextManager : MonoBehaviour
{
    private Coroutine showCoroutine;

    /// <summary>
    /// 文字送り演出を表示する
    /// </summary>
    public void Show(TMP_Text explanationText, AudioClip voice)
    {
        //テキストが空、もしくは音声が空なら何もしない
        if (explanationText.text == "")
        {
            Debug.Log("テキストが空です");
            return;
        }
        else if(voice == null)
        {
            Debug.Log("音声が空です");
            return;
        }

        // 前回の演出処理が走っていたら、停止
        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }

        // １文字ずつ表示する演出のコルーチンを実行する
        showCoroutine = StartCoroutine(ShowCoroutine(explanationText, voice));
    }

    // １文字ずつ表示する演出のコルーチン
    IEnumerator ShowCoroutine(TMP_Text explanationText, AudioClip voice)
    {
        // テキスト全体の長さ
        var textLength = explanationText.text.Length;
        //流れている音声の長さを取得
        var voiceLength = voice.length;
        // GC Allocを最小化するためキャッシュしておく
        var delay = new WaitForSeconds(voiceLength / textLength);
        
        // １文字ずつ表示する演出
        for (var i = 0; i < textLength; i++)
        {
            // 徐々に表示文字数を増やしていく
            explanationText.maxVisibleCharacters = i;
            
            // 一定時間待機
            yield return delay;
        }

        // 演出が終わったら全ての文字を表示する
        explanationText.maxVisibleCharacters = textLength;

        showCoroutine = null;
    }
}
