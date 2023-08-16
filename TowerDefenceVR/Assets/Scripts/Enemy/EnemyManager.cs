using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private GameObject enemies;

    List<GameObject> enemyList = new List<GameObject>();

    private void Start() 
    {
        foreach(Transform enemy in enemies.GetComponentInChildren<Transform>())
        {
            if(enemy.gameObject.CompareTag("Enemy"))
            {
                enemyList.Add(enemy.gameObject);
            }
        }
        Debug.Log(enemyList.Count);
    }

    private void Update() 
    {
        if(tutorialManager.AllEnemyKilledCoroutineStarted)
        {
            enemyList.Clear();
            foreach(Transform enemy in enemies.GetComponentInChildren<Transform>())
            {
                if(enemy.gameObject.CompareTag("Enemy"))
                {
                    enemyList.Add(enemy.gameObject);
                }
            }
            //チュートリアル : 全ての敵を倒したかどうか
            if(enemyList.Count == 0)
            {
                tutorialManager.KilledTrigger = true;
            }

        }
        else if(tutorialManager.HalfEnemyKilledCoroutineStarted)
        {
            enemyList.Clear();
            foreach(Transform enemy in enemies.GetComponentInChildren<Transform>())
            {
                if(enemy.gameObject.CompareTag("Enemy"))
                {
                    enemyList.Add(enemy.gameObject);
                }
            }
            //チュートリアル : 1体の敵を倒したかどうか
            if(enemyList.Count <= 4)
            {
                tutorialManager.KilledTrigger = true;
            }
        }
    }
}
