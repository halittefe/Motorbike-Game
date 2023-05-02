using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeAmount = 0.1f;
    public float shakeDuration = 0.1f;

    private Vector3 originalPosition;
    BossAI bossAI;



    private void Awake()
    {
        bossAI = GameObject.Find("BossEnemy").GetComponent<BossAI>();
    }
    void Update()
    {
        if (bossAI != null)
        {
            
            if (bossAI.isShoot == true)
            {
                StartCoroutine(Shake());
            }
            
        }
    }

    IEnumerator Shake()
    {
        originalPosition = transform.localPosition;
        float elapsed = 0.0f;
        while (elapsed < shakeDuration)
        {
            float x = Random.Range(originalPosition.x + 0.1f, originalPosition.x - 0.1f) ;
           // float y = Random.Range(9.2f, 10.2f) * shakeAmount;
            transform.localPosition = new Vector3(x, originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;

        
    }
}
