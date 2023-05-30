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
        "初めまして！これからこのゲームのルールと遊び方を説明します！",
        "まずは基本的な操作方法からです。手元を見てみてください。",
        "この空間上を移動するには右のスティックを使用します！今、点滅しているのが右のスティックです！動かして少し動いてみましょう！",
        "素晴らしいです！では、次に左のスティックを見てください。左のスティックを使用することでその場で回転することができます。試してみましょう！",
        "いいですね！基本的にはこの2つの操作ができれば、自由にステージ内を移動できます！では、次にこのゲームのルールを説明します。",
        "このゲームはステージ奥から現れる敵を倒して、自陣側に設置されているビーコンを破壊されないように守ることが目標です！",
        "敵を倒す方法は剣と魔法の2種類があります。最初に剣で敵を倒す方法を説明します！今、青色の矢印で示した場所に剣があるので、移動して近づいてみてください！",
        "それでは、まずは剣の掴み方からです。右のコントローラーを見てください！今、赤く点滅しているボタンがグリップボタン、青く点滅しているボタンがトリガーボタンです。",
        "この2つのボタンを同時に押すと手が掴む動作をします。この動作を剣の柄の部分ですることで剣をつかめます！やってみましょう！",
        "掴めたようですね！では、この剣をしまってみましょう！今、右のコントローラーで点滅しているBボタンでしまうことができます！",
        "一度しまった武器は同様にBボタンでいつでも取り出すことができます！",
        "おっと、森の奥にモンスターが見えますね。試しに剣を使って倒してみましょう！",
        "剣を敵に向かって振ると攻撃できます！やってみましょう！",
        "お見事です！では、このまま魔法の生成にもチャレンジしてみましょう！剣は一度Bボタンでしまってもらって大丈夫です！",
        "魔法は遠距離からでも攻撃できる点で優れていますが、使用するには詠唱が必要となります。詠唱は生成する魔法によって長さが変わります。",
        "ですが、一度生成した魔法は一定の数までは詠唱を省略して使うことができるので様々な種類の魔法を生成することをお勧めします！",
        "では、早速魔法を生成してみましょう！左のコントローラーを見てください！魔法は今、青く点滅している、Yボタンで生成します。",
        "どんな魔法でも大丈夫です！オリジナルの魔法名を入力し、生成ボタンを押して詠唱を開始しましょう！",
        "詠唱中です！左の手のひらを前に向け、魔法を放つ準備をしてください！",
        "お見事です！一度生成した魔法は、一定の数まではストックされ、詠唱なしで即座に打つことができます！",
        "左の手のひらを前に向けることで、生成済みの魔法を選択できます。先ほど生成した魔法を放ってみましょう！",
        "素晴らしいです！魔法を使用することで、遠くの敵にもダメージを与えられるので積極的に使ってみましょう！",
        "おや、どうやら森のさらに奥にもモンスターがいるようですね。試しに魔法で敵を倒してみましょう！" 
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
        //Listが空、もしくはaudioTrackが取得できなければ何もしない
        if(explanationTextList.Count == 0)
        {
            return;
        }
        else if(audioTrack == null)
        {
            Debug.Log("audioTrackが空です");
            return;
        }

        //最初のテキストを取得し、Listから削除
        nextText = explanationTextList[0];
        explanationTextList.RemoveAt(0);
        explanationText.text = nextText;
        
        //現在再生中の音声を取得する
        var currentVoice = GetCurrentAudioClip();
        if(currentVoice == null)
        {
            Debug.Log("currentVoiceが空です");
            return;
        }

        //テキスト送りを開始する
        dialogTextManager.Show(explanationText, currentVoice);
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
