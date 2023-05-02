using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject shopPanel;
    
    public Transform SpawnPoint;

    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Player"))
        {
            shopPanel.SetActive(true);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = SpawnPoint.position;
            player.transform.rotation = SpawnPoint.rotation;

            Time.timeScale = 0f;
        }
    }
}
