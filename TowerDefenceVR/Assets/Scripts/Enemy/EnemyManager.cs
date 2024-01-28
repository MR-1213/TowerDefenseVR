using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainStage
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyList = new List<GameObject>();
        public List<GameObject> generatedEnemyList = new List<GameObject>();
        float elapsedTime = 0f;

        private void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > 10f)
            {
                elapsedTime = 0f;
                GenerateEnemy();
            }
        }

        private void GenerateEnemy()
        {
            int randomIndex = Random.Range(1, 11);
            Vector3 randomPosition = new Vector3(Random.Range(10f, 50f), 0f, Random.Range(65f, 75f));
            if(randomIndex < 7)
            {
                GameObject enemy = Instantiate(enemyList[0], randomPosition, Quaternion.identity);
            }
            generatedEnemyList.Add()
        }
    }
}
