using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int coinValue;
    GameManager GameManager;
    BossAI bossAI;
    
    
    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        HealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }        
    }
    
    public void Die()
    {     
        GameManager.RestartPanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.RestartButton.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameManager.goldprice += coinValue;
            GameManager.goldpricetext.text = GameManager.goldprice + "";
        }
    }

    public void HealthBar()
    {
        GameManager.HealthBarSprite.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
