using System.Collections.Generic;
using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public GameObject playerBulletPrefab;
    public GameObject enemyBulletPrefab;
    public int poolSize;
    private Queue<GameObject> playerBulletPool;
    private Queue<GameObject> enemyBulletPool;

    private void Awake()
    {
        playerBulletPool = new Queue<GameObject>();
        enemyBulletPool = new Queue<GameObject>();

        // Create player bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform);
            bullet.SetActive(false);
            playerBulletPool.Enqueue(bullet);
        }

        // Create enemy bullets
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform);
            bullet.SetActive(false);
            enemyBulletPool.Enqueue(bullet);
        }
    }

    public GameObject GetPlayerBullet()
    {
        if (playerBulletPool.Count > 0)
        {
            GameObject bullet = playerBulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(playerBulletPrefab, transform);
            return bullet;
        }
    }

    public void ReturnPlayerBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        playerBulletPool.Enqueue(bullet);
    }

    public GameObject GetEnemyBullet()
    {
        if (enemyBulletPool.Count > 0)
        {
            GameObject bullet = enemyBulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform);
            return bullet;
        }
    }

    public void ReturnEnemyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        enemyBulletPool.Enqueue(bullet);
    }
}
