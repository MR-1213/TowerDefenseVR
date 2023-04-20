using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    
    private float grabSwordTime = 0f; //剣を掴んだ時間を計測する
    private Vector3 respawnPosition; //剣を落とした時に戻すリスポーン位置

    private void Start() 
    {
        respawnPosition = transform.position + new Vector3(0f, 0.3f, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        //チュートリアル : プレイヤーが家の中から出られないことを伝える
        if(other.gameObject.CompareTag("Player") && gameObject.name == "HouseBarrier")
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.DOFade(0.3f, 0.5f);
            
        }
        //チュートリアル : 剣の近くに移動したことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "TableWithSword")
        {
            tutorialManager.MovedTrigger = true;
        }

        //チュートリアル : 剣を落としたことを検知し、剣を元の位置に戻す
        if(other.gameObject.CompareTag("Ground") && gameObject.CompareTag("PlayerSword"))
        {
            var rigidbody = gameObject.GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            transform.parent.gameObject.transform.position = respawnPosition;
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

    private void OnCollisionEnter(Collision other) 
    {
        
    }
}
