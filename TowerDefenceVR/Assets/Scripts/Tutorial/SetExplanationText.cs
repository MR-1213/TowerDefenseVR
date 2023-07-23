using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using TMPro;

/// <summary>
/// チュートリアル中に表示されるテキストを保持するクラス
/// </summary>
public class SetExplanationText : MonoBehaviour
{
    [SerializeField] private DialogTextManager dialogTextManager;
    [SerializeField] private PlayableDirector director;

    private TimelineAsset timeline;
    private AudioTrack audioTrack;
    private TMP_Text explanationText;
    private string nextText;

    private List<string> explanationTextList = new List<string>()
    {
        "お～い、いつまで寝てるつもりだ？",
        "もう訓練が始まる時間だぞ！今日が初めての訓練だというのに大丈夫なのか...？",
        "まあ、いい。昨日は歓迎だと言って少し飲みすぎてしまったからな...大目に見ておこう",
        "さて、早速訓練場に向かうぞ。二日酔いで歩けないなんてことはないだろうな？",
        "よし、行くぞ。ついてこい。",
        "訓練場に着いたら、まずは剣の訓練、君は魔法も使えるようだからその次に魔法の訓練もするぞ",
        "よし、早速剣の訓練から始める。そこの机にある剣を持ってみろ。"
    };

    private void Start()
    {
        explanationText = GetComponent<TMP_Text>();

        // TimelineAssetを取得
        timeline = (TimelineAsset)director.playableAsset;
        // AudioTrackを取得
        foreach (TrackAsset trackAsset in timeline.GetOutputTracks())
        {
            if (trackAsset is AudioTrack)
            {
                audioTrack = (AudioTrack)trackAsset;
                break;
            }
        }
    }

    public void SetText()
    {
        //Listが空なら何もしない
        if(explanationTextList.Count == 0)
        {
            return;
        }

        //最初のテキストを取得し、Listから削除
        nextText = explanationTextList[0];
        explanationTextList.RemoveAt(0);
        explanationText.text = nextText;

        //テキスト送りを開始する
        dialogTextManager.Show(explanationText);
    }

    private AudioClip GetCurrentAudioClip()
    {
        foreach (TimelineClip clip in audioTrack.GetClips())
        {
            if (clip.start <= director.time && clip.end > director.time)
            {
                AudioPlayableAsset audioPlayableAsset = clip.asset as AudioPlayableAsset;
                AudioClip currentClip = audioPlayableAsset.clip;
                return currentClip;
            }
        }

        return null;
    }
}
