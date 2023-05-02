using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private EnemySpawner[] spawners;
    private int totalEnemies;
    private bool shouldResumeSpawning;

    public int resumeSpawningThreshold = 20;

    private void Start()
    {
        spawners = GetComponentsInChildren<EnemySpawner>();
        StartCoroutine(CountEnemies());
    }

    private IEnumerator CountEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            int count = 0;
            foreach (EnemySpawner spawner in spawners)
            {
                count += spawner.currentEnemies;
            }
            totalEnemies = count;

            if (totalEnemies >= 40)
            {
                shouldResumeSpawning = false;
                foreach (EnemySpawner spawner in spawners)
                {
                    spawner.enabled = false;
                }
            }
            else if (totalEnemies <= resumeSpawningThreshold && !shouldResumeSpawning)
            {
                shouldResumeSpawning = true;
                foreach (EnemySpawner spawner in spawners)
                {
                    spawner.enabled = true;
                }
            }
        }
    }
}
