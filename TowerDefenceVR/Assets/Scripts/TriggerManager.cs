using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private Transform swordRespawnPos;
    
    private float grabSwordTime = 0f; //剣を掴んだ時間を計測する
    private Vector3 respawnPosition; //剣を落とした時に戻すリスポーン位置

    /* オブジェクトによってStart関数でコンポーネントが取得されるかどうかが変わる変数群 */
    private Renderer barrierRenderer; //HouseBariierオブジェクトのRenderer
    private CanvasGroup canvasGroup; //HouseBariierオブジェクトのCanvasGroup
    private Rigidbody swordRigidbody; //SwordオブジェクトのRigidbody

    private void Start() 
    {
        //HouseBarrierオブジェクトの場合はRendererとCanvasGroupを取得する
        if(gameObject.name == "HouseBarrier" || gameObject.name == "FenceBarrier")
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
        //チュートリアル : プレイヤーが柵の外から出られないことを伝える表示を出す
        if(other.gameObject.CompareTag("Player") && gameObject.name == "FenceBarrier")
        {
            barrierRenderer.material.DOFade(0.3f, 0.5f);
            canvasGroup.DOFade(1.0f, 0.5f);
        }
        //チュートリアル : 剣を落としたことを検知し、剣を元の位置に戻す
        if(other.gameObject.CompareTag("Ground") && gameObject.CompareTag("PlayerSword"))
        {
            swordRigidbody.velocity = Vector3.zero;
            transform.parent.gameObject.transform.position = swordRespawnPos.position;
            grabSwordTime = 0f;
        }

        //チュートリアル : 敵に近づいたことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "EnemyTriggerManager")
        {
            tutorialManager.MovedTrigger = true;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        //チュートリアル : 剣を掴んだことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("PlayerSword"))
        {
            grabSwordTime += Time.deltaTime;
            if(grabSwordTime > 2.0f)
            {
                tutorialManager.GrabbedTrigger = true;
                grabSwordTime = 0f;
            }
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
}
