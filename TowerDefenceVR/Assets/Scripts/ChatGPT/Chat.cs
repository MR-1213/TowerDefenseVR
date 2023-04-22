using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] TMP_InputField inputField; //魔法の入力フィールド
    [SerializeField] Button generateButton; //生成ボタン
    [SerializeField] TMP_Text errorMessage; //エラー時のメッセージ
    [SerializeField] string apiKey; // NOTE: 入力したままコミットやリポジトリの公開などをしないこと、公開した場合自動的に無効になる。

    OpenAIChatCompletionAPI chatCompletionAPI;
    SearchMagicInformation searchMagicInformation;
    GenerateMagic generateMagic;
    private GameObject currentMagic;

    //初期メッセージを定義
    List<OpenAIChatCompletionAPI.Message> context = new List<OpenAIChatCompletionAPI.Message>()
    {
        new OpenAIChatCompletionAPI.Message(){
            role = "system", 
            content = @"あなたは魔法の創造者です。私が挙げる魔法名に対して以下の情報を推察し、次に指定するフォーマットで回答してください。
                        {
                        回答のフォーマット:
                        属性:<魔法名から推察された属性>:attributeEnd
                        威力:<魔法名から推察された威力の値(001-100)>:powerEnd
                        }
                        <魔法名から推察された属性>は<火><氷><水><雷><風><土><光><闇>のいずれかとします。推察できない魔法であった場合は<無>としてください。
                        <魔法名から推察された威力>は最も弱いものを001、最も強いものを100として整数値で出来るだけ小さい値を推察してください。数値は必ず3桁で示してください。

                        回答のフォーマットに即した回答のみ答えてください。
                        次の会話からスタートします"
        }
    };

    void Awake()
    {
        chatCompletionAPI = new OpenAIChatCompletionAPI(apiKey);
    }

    private void Start() 
    {
        generateMagic = GetComponent<GenerateMagic>();
    }

    public void OnGenerateButtonClick()
    {
        //何も入力されていない、もしくはNullであれば終了
        if (string.IsNullOrEmpty(inputField.text)) return;
        //新たな魔法の入力を受け付ける
        var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = inputField.text };
        context.Add(message);
        //入力フィールドを空にする
        inputField.text = "";
        //前回生成した魔法を削除する
        if(currentMagic != null)
        {
            Destroy(currentMagic);
        }
        //エラーメッセージを空にする
        errorMessage.text = "";
        //ChatGPTとの通信準備開始
        StartCoroutine(ChatCompletionRequest());
    }

    public void MagicInvisibility()
    {
        if(currentMagic != null)
        {
            currentMagic.SetActive(false);
        }
    }

    public void MagicVisibility()
    {
        if(currentMagic != null)
        {
            currentMagic.SetActive(true);
        }
    }

    IEnumerator ChatCompletionRequest()
    {
        //回答が帰ってくるまでは新たな生成は受け付けない
        generateButton.interactable = false;

        //1フレーム中断して再開する(非同期処理)
        yield return null;

        //入力内容から新たなリクエストを生成する
        var request = chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = context }
        );
        //リクエストを送信する
        yield return request.Send();
        //レスポンスがエラーであった場合はエラー処理
        if (request.IsError)
        {
            GenerateErrorMessage();
            yield break;
        }
        //レスポンスのリストの中から最も新しいレスポンス内容を取得する
        var message = request.Response.choices[0].message;

        //レスポンスから魔法の情報を取得する
        searchMagicInformation = new SearchMagicInformation(message.content);
        string magicInfoKey = searchMagicInformation.GetMagicInfo();
        if(string.IsNullOrEmpty(magicInfoKey))
        {
            GenerateErrorMessage();
            yield break;
        }

        //魔法の生成をする
        GameObject magic = generateMagic.Generate(magicInfoKey);
        if(magic == null)
        {
            GenerateErrorMessage();
            yield break;
        }
        //currentMagic = magic;

        //1フレーム中断して再開(非同期処理)
        yield return null;

        //チュートリアル再開
        tutorialManager.GeneratedTrigger = true;
        //新たな魔法の生成を受け付けられるようにする
        generateButton.interactable = true;
    }

    private void GenerateErrorMessage()
    {
        errorMessage.text = "魔法が上手く生成できませんでした...\nもう一度お試しください";
        generateButton.interactable = true;
    }
}
