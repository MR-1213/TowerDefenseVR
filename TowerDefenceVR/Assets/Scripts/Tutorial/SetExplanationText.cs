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
    [SerializeField] private ConversationHistoryManager conversationHistoryManager;

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
        "そうだ、この駐屯地内にある3つの塔は、この駐屯地全体を守る防御結界の要としても機能している重要な施設なんだ",
        "何か問題が発生したらこの施設を最優先で守ってほしいということだけ覚えておいてくれ",
        "さて、改めて訓練場向かうとしよう",
        "ん？今の音は...？",
        "な...！？東塔が...！結界で防ぎきれなかったのか...！？",
        "まずい、とにかく奥の東の塔のもとまで急ぐぞ！",
        "くそっ、既に敵が侵入している！",
        "ぐずぐずしている暇はないな...奥の訓練場にいる敵を倒すぞ！すまないが、集団戦は早速実戦だ！",
        "敵にも魔法を使うやつがいるぞ！！物陰に隠れて攻撃を避けながら戦うんだ！",
        "よし、なんとか倒したな！いきなりの実戦にしてはやるじゃないか...！皆、こちらに集まってくれ！",
        "中に侵入してきた敵はこれで全部のようだが...妙だな、この程度の敵であの塔がやられるとは思えない...",
        "その件について一つご報告いたします！東塔が落とされましたが、西塔および中央塔は無事でした。",
        "そうか...ならばこの駐屯地が陥落するという最悪の事態は免れたか...",
        "しかし、気になる報告がありまして...東塔を落とした元凶は別にいるらしく、東塔に攻撃した後すぐにどこかに去ってしまったと..." +
        "その敵はここからさらに東の方角へ向かったようです",
        "厄介だな...ここから東にはいくつもの村や町がある。すぐにその敵を追う必要がありそうだな...",
        "いきなりで申し訳ないが、君にはその敵を追ってほしい。この駐屯地の防衛は任せておけ。なんとしても村民や町民に被害が出ることを防ぐんだ！"
    };

    private void Start()
    {
        explanationText = GetComponent<TMP_Text>();

        // TimelineAssetを取得
        timeline = (TimelineAsset)director.playableAsset;
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
        conversationHistoryManager.AddConversationHistory(nextText);
        explanationTextList.RemoveAt(0);
        explanationText.text = nextText;

        //テキスト送りを開始する
        dialogTextManager.Show(explanationText);
    }
}
