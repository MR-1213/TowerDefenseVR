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
        "魔法は遠くの敵にも有効だが、即座に攻撃できないのが難点だ。",
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
        "よし、なんとか倒したな！いきなりの実戦にしてはやるじゃないか...！",
        "まずい、東塔が集中的に狙われている...！すまないが、東塔の防衛に回ってくれ！",
        "東塔が完全に落とされる前に倒しきるんだ！",
        "よし、なんとか防衛には成功だな！一旦こちらに来てくれ！",
        "東塔を守ることができて本当に良かった...中央塔と西塔もどうやら無事らしい。",
        "しかし、気になる報告が先ほどあってな...別の敵の集団がここを無視して近隣の村や町の方面に向かったらしいんだ",
        "すぐにでも全員で追いかけたいが、ここがまた攻撃を受けるとも分からないか...",
        "よし、いきなりで申し訳ないが、君にはその敵を追ってほしい。この駐屯地の防衛は任せておけ。なんとしても村民や町民に被害が出ることを防ぐんだ！"
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
