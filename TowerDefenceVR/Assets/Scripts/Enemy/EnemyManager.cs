using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainStage
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set;}

        [Header("敵のリスト")]
        [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
        private List<GameObject> spawnedEnemyList = new List<GameObject>();
        float elapsedTime = 10f; // ゲーム経過時間

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 10f)
            {
                elapsedTime = 0f;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            // 同時に出現する敵の数を制限
            if(spawnedEnemyList.Count >= 5)
            {
                // スポーンさせない
                return;
            }

            int randomIndex = Random.Range(0, enemyList.Count);
            Vector3 randomPosition = new Vector3(Random.Range(10f, 50f), 0f, Random.Range(65f, 75f));
            
            GameObject enemy = Instantiate(enemyList[randomIndex], randomPosition, Quaternion.identity);
            spawnedEnemyList.Add(enemy);  
        }
    }
}
