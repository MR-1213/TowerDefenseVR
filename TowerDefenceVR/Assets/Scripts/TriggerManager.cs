using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //チュートリアル : 剣の近くに移動したことを検知する
        if(other.gameObject.CompareTag("Player") && gameObject.name == "TableWithSword")
        {
            tutorialManager.MovedTrigger = true;
        }

        //チュートリアル : 剣を落としたことを検知し、剣を元の位置に戻す
        if(other.gameObject.CompareTag("Ground") && transform.parent.gameObject.name == "Sword")
        {
            transform.parent.gameObject.transform.position = respawnPosition;
            grabSwordTime = 0f;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        //チュートリアル : 剣を掴んだことを検知する
        if(other.gameObject.CompareTag("Player") && transform.parent.gameObject.name == "Sword")
        {
            grabSwordTime += Time.deltaTime;
            if(grabSwordTime > 3.0f)
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
