using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 15;
    public int currentEnemies;
    private bool SpawnCheck = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (SpawnCheck)
        {
            yield return new WaitForSeconds(5.0f);

            if (currentEnemies >= maxEnemies)
            {
                break;
                  
            }

           

            GameObject newEnemyObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            EnemyAI newEnemy = newEnemyObject.GetComponent<EnemyAI>();
            newEnemy.OnEnemyDeath += EnemyDestroyed;
            currentEnemies++;
        }

        
    }

    public void EnemyDestroyed(EnemyAI enemy)
    {
        currentEnemies--;
        enemy.OnEnemyDeath -= EnemyDestroyed;
    }
}
