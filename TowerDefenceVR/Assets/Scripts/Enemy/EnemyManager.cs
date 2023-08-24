using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private GameObject firstEnemies;
    [SerializeField] private GameObject secondEnemies;
    
    public AudioClip AttackedSE;

    List<GameObject> firstEnemyList = new List<GameObject>();
    List<GameObject> secondEnemyList = new List<GameObject>();

    private void Start() 
    {
        foreach(Transform enemy in firstEnemies.GetComponentInChildren<Transform>())
        {
            if(enemy.gameObject.CompareTag("Enemy"))
            {
                firstEnemyList.Add(enemy.gameObject);
            }
        }
        
        foreach(Transform enemy in secondEnemies.GetComponentInChildren<Transform>())
        {
            if(enemy.gameObject.CompareTag("Enemy"))
            {
                secondEnemyList.Add(enemy.gameObject);
            }
        }
    }

    private void Update() 
    {
        if(tutorialManager.AllCoroutineStarted)
        {
            firstEnemyList.Clear();
            foreach(Transform enemy in firstEnemies.GetComponentInChildren<Transform>())
            {
                if(enemy.gameObject.CompareTag("Enemy"))
                {
                    firstEnemyList.Add(enemy.gameObject);
                }
            }
            //チュートリアル : 全ての敵を倒したかどうか
            if(firstEnemyList.Count == 0)
            {
                tutorialManager.KilledTrigger = true;
            }

        }
        else if(tutorialManager.HalfCoroutineStarted)
        {
            firstEnemyList.Clear();
            foreach(Transform enemy in firstEnemies.GetComponentInChildren<Transform>())
            {
                if(enemy.gameObject.CompareTag("Enemy"))
                {
                    firstEnemyList.Add(enemy.gameObject);
                }
            }
            //チュートリアル : 1体の敵を倒したかどうか
            if(firstEnemyList.Count <= 4)
            {
                tutorialManager.KilledTrigger = true;
            }
        }
        else if(tutorialManager.AdditionalCoroutineStarted)
        {
            secondEnemyList.Clear();
            foreach(Transform enemy in secondEnemies.GetComponentInChildren<Transform>())
            {
                if(enemy.gameObject.CompareTag("Enemy"))
                {
                    secondEnemyList.Add(enemy.gameObject);
                }
            }
            //チュートリアル : 1体の敵を倒したかどうか
            if(secondEnemyList.Count == 0)
            {
                tutorialManager.KilledTrigger = true;
            }
        }
    }

    public AudioClip GetAttackedSE()
    {
        return AttackedSE;
    }
}
