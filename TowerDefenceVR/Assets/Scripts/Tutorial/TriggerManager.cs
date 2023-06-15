using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//チュートリアルで使用する各種トリガーを管理するクラス
public class TriggerManager : MonoBehaviour
{
    private TutorialManager tutorialManager;
    private PlayerWeaponManager playerWeaponManager;
    private PlayerControllerManager playerControllerManager;
    
    private Vector3 respawnPosition; //剣を落とした時に戻すリスポーン位置

    /* オブジェクトによってStart関数でコンポーネントが取得されるかどうかが変わる変数群 */
    private Renderer barrierRenderer; //HouseBariierオブジェクトのRenderer
    private CanvasGroup canvasGroup; //HouseBariierオブジェクトのCanvasGroup
    private Rigidbody swordRigidbody; //SwordオブジェクトのRigidbody

    private float grabbingTime = 0f;
    public bool isInProcess { get; set; } = true;

    private void Start() 
    {
        tutorialManager = GameObject.Find("TutorialExplanationManager").GetComponent<TutorialManager>();
        playerWeaponManager = GameObject.Find("OVRPlayerController").GetComponent<PlayerWeaponManager>();
        playerControllerManager = GameObject.Find("OVRPlayerController").GetComponent<PlayerControllerManager>();

        //HouseBarrierオブジェクトの場合はRendererとCanvasGroupを取得する
        if(gameObject.name == "HouseBarrier" || gameObject.CompareTag("FenceBarrier"))
        {
            barrierRenderer = gameObject.GetComponent<Renderer>();
            canvasGroup = gameObject.GetComponentInChildren<CanvasGroup>();
        }
        //Swordオブジェクトの場合はRigidbodyを取得する
        else if(gameObject.CompareTag("PlayerSword"))
        {
            swordRigidbody = gameObject.GetComponentInParent<Rigidbody>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //チュートリアル : プレイヤーが家の中から出られないことを伝える表示を出す
        if(other.gameObject.CompareTag("Player") && gameObject.name == "HouseBarrier")
        {
            barrierRenderer.material.DOFade(0.3f, 0.5f);
            canvasGroup.DOFade(1.0f, 0.5f);
        }
        //チュートリアル : 剣の近くに移動したことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "TableWithSword")
        {
            tutorialManager.MovedTrigger = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        //チュートリアル : 剣を掴んでいることを検知する
        if(other.gameObject.CompareTag("PlayerHand") && gameObject.CompareTag("PlayerSword"))
        {
            if(!isInProcess)
            {
                isInProcess = true;
                StartCoroutine(CountGrabbingSwordTime());
            }
        }

        //チュートリアル : プレイヤーが柵の外から出られないことを伝える表示を出す
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("FenceBarrier"))
        {
            barrierRenderer.material.DOFade(0.3f, 0.5f);
            canvasGroup.DOFade(1.0f, 0.5f);
        }
        //チュートリアル : 剣を落としたことを検知し、剣を元の位置に戻す
        if(other.gameObject.CompareTag("Ground") && gameObject.CompareTag("PlayerSword"))
        {
            Destroy(gameObject.transform.parent.gameObject);
            playerControllerManager.isGrabWeapon = false;
            playerControllerManager.grabWeapon = null;
        }

        //チュートリアル : 敵に近づいたことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "EnemyTriggerManager")
        {
            tutorialManager.MovedTrigger = true;
        }

        //チュートリアル : 荷馬車に近づいたことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "GettingCloseMultipleMonsters")
        {
            tutorialManager.MovedTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        //チュートリアル : プレイヤーが家の中から出られないことを伝える表示を消す
        if(other.gameObject.CompareTag("Player") && gameObject.name == "HouseBarrier")
        {
            barrierRenderer.material.DOFade(0f, 0.5f);
            canvasGroup.DOFade(0f, 0.5f);
        }

        //チュートリアル : プレイヤーが柵の外から出られないことを伝える表示を消す
        if(other.gameObject.CompareTag("Player") && gameObject.name == "FenceBarrier")
        {
            barrierRenderer.material.DOFade(0f, 0.5f);
            canvasGroup.DOFade(0f, 0.5f);
        }
    }

    IEnumerator CountGrabbingSwordTime()
    {
        grabbingTime = 0f;
        while(true)
        {
            if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) &&
               OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                grabbingTime += Time.deltaTime;
            }
            else if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) &&
                    OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                isInProcess = false;
                yield break;
            }

            if(grabbingTime > 2.0f)
            {
                tutorialManager.GrabbedTrigger = true;
                playerWeaponManager.AddWeapon(gameObject.transform.parent.gameObject);

                playerControllerManager.isGrabWeapon = true;
                playerControllerManager.grabWeapon = gameObject.transform.parent.gameObject;

                break;
            }

            yield return null;
        }
    }
}
