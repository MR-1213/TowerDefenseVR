using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    //[SerializeField] ChatMessageView messageViewTemplete;
    public TMP_Text debugText;
    public TMP_Text test1Text;
    public TMP_Text test2Text;
    [SerializeField] TMP_InputField inputField; //魔法の入力フィールド
    //[SerializeField] ScrollRect scrollRect;
    [SerializeField] Button generateButton; //生成ボタン
    [SerializeField] string apiKey; // NOTE: 入力したままコミットやリポジトリの公開などをしないこと

    OpenAIChatCompletionAPI chatCompletionAPI;
    SearchMagicInformation searchMagicInformation;
    GenerateMagic generateMagic;

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
                        <魔法名から推察された威力>は最も弱いものを001、最も強いものを100として整数値で小さい値を推察してください。数値は必ず3桁で示してください。

                        回答のフォーマットに即した回答のみ答えてください。
                        次の会話からスタートします"
        }
    };

    void Awake()
    {
        //messageViewTemplete.gameObject.SetActive(false);
        //generateButton.onClick.AddListener(OnSendClick);
        chatCompletionAPI = new OpenAIChatCompletionAPI(apiKey);
    }

    private void Start() 
    {
        generateMagic = GetComponent<GenerateMagic>();
        //新たな魔法の入力を受け付ける
        var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = "ファイアボール" };
        context.Add(message);
        //AppendMessage(message);
        inputField.text = "";
        //ChatGPTとの通信準備開始
        StartCoroutine(ChatCompletionRequest());
    }

    public void OnGenerateButtonClick()
    {
        //何も入力されていない、もしくはNullであれば終了
        if (string.IsNullOrEmpty(inputField.text)) return;
        //新たな魔法の入力を受け付ける
        var message = new OpenAIChatCompletionAPI.Message() { role = "user", content = inputField.text };
        context.Add(message);
        //AppendMessage(message);
        inputField.text = "";
        //ChatGPTとの通信準備開始
        StartCoroutine(ChatCompletionRequest());
    }

    IEnumerator ChatCompletionRequest()
    {
        //回答が帰ってくるまでは新たな生成は受け付けない
        generateButton.interactable = false;

        //1フレーム中断して再開する(非同期処理)
        yield return null;
        //scrollRect.verticalNormalizedPosition = 0;
        //入力内容から新たなリクエストを生成する
        var request = chatCompletionAPI.CreateCompletionRequest(
            new OpenAIChatCompletionAPI.RequestData() { messages = context }
        );
        //リクエストを送信する
        yield return request.Send();
        //レスポンスがエラーであった場合はエラー処理
        if (request.IsError) throw new System.Exception(request.Error);
        //レスポンスのリストの中から最も新しいレスポンス内容を取得する
        var message = request.Response.choices[0].message;

        //testText.text = "";
        test1Text.text = message.content;
        searchMagicInformation = new SearchMagicInformation(message.content);
        string magicInfoKey = searchMagicInformation.GetMagicInfo();
        if(string.IsNullOrEmpty(magicInfoKey))
        {
            magicInfoKey = "魔法名が見つかりませんでした";
        }
        test2Text.text = magicInfoKey;

        GameObject magic = generateMagic.Generate(magicInfoKey);
        debugText.text = magic.name;

        //AppendMessage(message);
        //1フレーム中断して再開(非同期処理)
        yield return null;
        //scrollRect.verticalNormalizedPosition = 0;
        //新たな魔法の生成を受け付けられるようにする
        generateButton.interactable = true;
    }

    /*
    void AppendMessage(OpenAIChatCompletionAPI.Message message)
    {
        context.Add(message);
        
        var messageView = Instantiate(messageViewTemplete);
        messageView.gameObject.name = "message";
        messageView.gameObject.SetActive(true);
        messageView.transform.SetParent(messageViewTemplete.transform.parent, false);
        messageView.Role = message.role;
        messageView.Content = message.content;

        if(message.role != "user")
        {
            //saveScript.ExtractCode(message.content);
        }
        
    }
    */
}
