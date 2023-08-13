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
        "よし、早速剣の訓練から始める。そこの机にある剣を持ってみろ。",
        "剣はどんな敵にも通用する武器だ！その代わり、敵に近づく必要があるというデメリットもあるがな",
        "よし、奥にいる盾持ちが練習相手になってくれる。盾の耐久力が半分になるまで攻撃してみるんだ！",
        "なかなか筋がいいな！一度こちらに戻って来てくれ！",
        "今度は魔法で遠距離から攻撃してみろ。魔法の属性は問わない、上手く狙いを定めるんだ！" +
        "一度詠唱した魔法は無詠唱で打つこともできるからな、そっちも試してみるといいぞ",
        "よし、上手いぞ！敵によって弱点の属性は異なるから上手くいろいろな属性の魔法を使い分けるんだ",
        "さて、今度は集団戦の練習をするから少し移動するぞ、ここより広い訓練場があるからそこに向かう",
        "こっちだ、ついてきてくれ",
        "そうだ、この左の塔はこの駐屯地全体を守る防御結界の要としても機能している重要な施設なんだ",
        "何か問題が発生したらこの施設を最優先で守ってほしいということだけ覚えておいてくれ",
        "さて、改めて訓練場向かうとしよう",
        "ん？今の音は...？",
        "な...！？東の塔が...！結界で防ぎきれなかったのか...！？",
        "まずい、とにかく奥の東の塔のもとまで急ぐぞ！",
        "くそっ、既に敵が侵入している！",
        "ぐずぐずしている暇はないな...奥の訓練場にいる敵を倒すぞ！すまないが、集団戦は早速実戦だ！"
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
