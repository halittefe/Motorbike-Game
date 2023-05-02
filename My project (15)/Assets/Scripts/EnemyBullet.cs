using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    private float lifetime;
    private int damage;
    private Vector3 target;
    private Vector3 direction;
    private bool isMoving;

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetLifetime(float lifetime)
    {
        this.lifetime = lifetime;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
        direction = (target - transform.position).normalized;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerHealth = other.gameObject.GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
