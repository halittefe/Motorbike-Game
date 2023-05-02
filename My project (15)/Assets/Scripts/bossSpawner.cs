using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpawner : MonoBehaviour
{
    public GameObject enemyBossPrefab;
    public int deadEnemyForBoss = 10;
    private bool bossSpawned = false;
    private GameManager gm;
    private PanelManager pm;
    public GameObject BossEnemy;

    private void Start()
    {
        BossEnemy.SetActive(false);
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        pm = GameObject.Find("GameManager").GetComponent<PanelManager>();
    }
    void Update()
    {
        //optimise it
        if(bossSpawned == false)
        {
            if (gm.deadEnemyCounter >= deadEnemyForBoss)
            {
                Debug.Log("boss spawn");
                BossSpawn();
                bossSpawned = true;
            }
        }
    }
    
    private void ActivateNextLevel()
    {
        pm.nextPanel.SetActive(true);
        pm.ParticleEffect.SetActive(true);
    }

    void BossSpawn()
    {
        BossEnemy.SetActive(true);
        
    }
}
