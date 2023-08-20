using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using Microsoft.Unity.VisualStudio.Editor;

/// <summary>
/// プレイヤーのコントローラー操作/ハンド操作を管理するクラス
/// </summary>
public class PlayerControllerManager : MonoBehaviour
{
    public Chat chat;

    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private GenerateMagic generateMagic;
    private PlayerWeaponManager playerWeaponManager;

    [SerializeField] private Transform underWristPos; //手首の下の位置
    [SerializeField] private Transform frontOfTheHandPos; //手のひらの前の位置
    [SerializeField] private SkinnedMeshRenderer controllerRenderer; //コントローラーのメッシュ
    [SerializeField] private CanvasGroup generatedMagicCanvasGroup; //生成済み魔法を表示するキャンバス

    public bool isGrabWeapon {private get; set;} = false; //武器を掴んでいるかどうかを表すフラグ
    public GameObject grabWeapon {private get; set;} //掴んでいる武器

    private GameObject gaugeImage;
    private bool isCanvasActive = false; //魔法生成UIが表示されているかを表すフラグ
    private bool isPushingGenerateButton = false; //生成ボタンを押しているかを表すフラグ
    private bool isGenerating = false; //魔法を生成中かを表すフラグ
    private float generateButtonPushingTime = 0f; //生成ボタンを押している時間
    private int index = 0; //生成済み魔法UIのインデックス

    private int testCount = 0;

    private void Start() 
    {
        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }

    private void Update() 
    {
        //Bボタンで剣をしまう/取り出す
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if(isGrabWeapon)
            {
                Debug.Log("剣をしまう");
                playerWeaponManager.PutawayWeapon(grabWeapon);
                isGrabWeapon = false;
            }
            else
            {
                Debug.Log("剣を取り出す");
                playerWeaponManager.TakeoutWeapon();
            }
        }

        //Yボタンで魔法生成UIを表示/非表示
        if(OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            playerUIManager.ChangeNewMagicCanvasEnable();
        }

        //左手のひらを前に向けたら生成済み魔法UIを表示する
        RaycastHit underWristHit;
        isCanvasActive = false;
        if(Physics.Raycast(underWristPos.position, -underWristPos.forward, out underWristHit, 3.0f))
        {
            //手首の下が地面を向いているかどうか
            if(underWristHit.collider.gameObject.CompareTag("Ground"))
            {
                foreach(RaycastHit backOfTheHandHit in Physics.RaycastAll(frontOfTheHandPos.position, -frontOfTheHandPos.forward, 1.5f, LayerMask.GetMask("MyPlayer"), QueryTriggerInteraction.UseGlobal))
                {
                    //手のひらの後ろ側にプレイヤーがいるかどうか
                    if(backOfTheHandHit.collider.gameObject.CompareTag("Player"))
                    {
                        //手のひらを前に向けていると判断し、生成済み魔法を表示する
                        //DOTweenの動作回数を抑えるため、一度だけ実行する
                        if( controllerRenderer.material.color.a == 1f)
                        {
                            controllerRenderer.material.DOFade(0f, 0.5f);
                            generatedMagicCanvasGroup.DOFade(1f, 0.5f);

                            playerUIManager.ChangeGeneratedMagicCanvasEnable();
                        
                        }
                        
                        isCanvasActive = true;
                        break;
                    }
                }

                //手のひらを前にむけていないときは、生成済み魔法を非表示にする
                if(!isCanvasActive && controllerRenderer.material.color.a == 0f)
                {
                    //DOTweenの動作回数を抑えるため、一度だけ実行する
                    controllerRenderer.material.DOFade(1f, 0.5f);
                    generatedMagicCanvasGroup.DOFade(0f, 0.5f);
                    playerUIManager.ChangeGeneratedMagicCanvasDisable();
                }
            }
            //手のひらを前に向けていないときは、生成済み魔法を非表示にする
            else if(controllerRenderer.material.color.a == 0f)
            {
                //DOTweenの動作回数を抑えるため、一度だけ実行する
                controllerRenderer.material.DOFade(1f, 0.5f);
                generatedMagicCanvasGroup.DOFade(0f, 0.5f);
                playerUIManager.ChangeGeneratedMagicCanvasDisable();
            }
        }

        //Xボタンで生成済み魔法選択、生成
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch) || isPushingGenerateButton)
        {
            if(!isCanvasActive) return;

            isGenerating = false;
            if(!isPushingGenerateButton)
            {
                //現在アクティブになっている生成済み魔法UIのオブジェクトを取得
                foreach(var child in generatedMagicCanvasGroup.GetComponentsInChildren<Transform>())
                {
                    if(child.gameObject.CompareTag("Gauge"))
                    {
                        gaugeImage = child.gameObject;
                        break;
                    }
                }
            }
            else
            {
                generateButtonPushingTime += Time.deltaTime;
                //ゲージを上昇させる
                gaugeImage.transform.localPosition = new Vector3(0f, -200f + 200f * generateButtonPushingTime / 2f, 0f);

                if(generateButtonPushingTime > 0.5f)
                {
                    isGenerating = true;
                }
            }

            isPushingGenerateButton = true;

            //Xボタン長押し(2秒)で生成
            if(generateButtonPushingTime > 2.0f)
            {
                //generatedMagicGroupの子オブジェクトでアクティブなものだけを取得
                GameObject selectedMagic = null;
                foreach(Transform child in generatedMagicCanvasGroup.GetComponentsInChildren<Transform>())
                {
                    if(child.gameObject.activeSelf && child.transform != generatedMagicCanvasGroup.transform)
                    {
                        selectedMagic = child.gameObject;
                        break;
                    }
                }

                if(selectedMagic != null) generateMagic.GenerateSavedMagic(selectedMagic);
                
                gaugeImage.transform.localPosition = new Vector3(0f, -200f, 0f);
                isGenerating = true;
                isPushingGenerateButton = false;
            }

        }

        if(OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch) && !isGenerating)
        {
            //次の生成済みUIをアクティブにする
            playerUIManager.NextGeneratedMagic();

            gaugeImage.transform.localPosition = new Vector3(0f, -200f, 0f);
            isPushingGenerateButton = false;
            generateButtonPushingTime = 0f;
        }
        else if(OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            gaugeImage.transform.localPosition = new Vector3(0f, -200f, 0f);
            isPushingGenerateButton = false;
            generateButtonPushingTime = 0f;
        }

        //デバッグ用 Aボタンで魔法生成
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            testCount++;
            if(testCount == 1) chat.DebugGenerateMagic1();
            if(testCount == 2) chat.DebugGenerateMagic2();
        }
    }
}
