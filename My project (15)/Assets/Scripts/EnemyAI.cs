using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Values")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinPivot;
    private Animator animator;
    public int currentHealth;
    public bool IsDead { get; private set; }
    private enum EnemyStatus { Idle, Chasing, Shooting, Dead };
    private EnemyStatus currentStatus = EnemyStatus.Idle;
    private GameManager gm;
    private BulletPooler bp;

    [Header("For Chasing")]
    private Transform playerTransform;
    [SerializeField] private float chaseDistance = 10f;
    [SerializeField] private float shootDistance = 5f;

    [Header("Bullet Stuff")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifeTime = 1.4f;
    [SerializeField] private float fireRate = 0.5f;

    public delegate void EnemyDeathEvent(EnemyAI enemy);
    public event EnemyDeathEvent OnEnemyDeath;

    private float lastShotTime = -1000f;
    private float nextFireTime;


    
    private void Start()
    {
        bp = GameObject.Find("Pooler").GetComponent<BulletPooler>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerTransform = GameObject.Find("Player").transform;
        currentHealth = maxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (IsDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= shootDistance)
        {
            ShootPlayer(distanceToPlayer);
        }
        else if (distanceToPlayer <= chaseDistance)
        {
            MoveTowardsPlayer();
            animator.SetBool("isShoot", false);
            animator.SetBool("isWalking", true);
            currentStatus = EnemyStatus.Chasing;
            transform.LookAt(playerTransform);
        }
        else
        {
            StopMoving();
            animator.SetBool("isWalking", false);
            animator.SetBool("isIdle", true);
            currentStatus = EnemyStatus.Idle;
        }
    }

    private void StopMoving()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
    }

    private void MoveTowardsPlayer()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(playerTransform.position);
    }

    private void ShootPlayer(float distanceToPlayer)
    {
        if (Time.time <= lastShotTime + bulletLifeTime || Time.time <= nextFireTime || IsDead)
            return;

        if (distanceToPlayer <= shootDistance)
        {
            currentStatus = EnemyStatus.Shooting;
            StopMoving();
            transform.LookAt(playerTransform);
            animator.SetBool("isShoot", true);
            Shoot(playerTransform.position);
        }
    }

    private void Shoot(Vector3 targetPosition)
    {
        targetPosition.y += 1.2f;
        GameObject bullet = bp.GetEnemyBullet();
        bullet.transform.position = firePoint.transform.position;
        bullet.transform.rotation = firePoint.transform.rotation;
        bullet.GetComponent<EnemyBullet>().SetLifetime(bulletLifeTime);
        bullet.GetComponent<EnemyBullet>().SetDamage(bulletDamage);
        bullet.GetComponent<EnemyBullet>().SetSpeed(bulletSpeed);
        bullet.GetComponent<EnemyBullet>().SetTarget(targetPosition);
        nextFireTime = Time.time + fireRate;
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (IsDead) return;

        IsDead = true;
        currentStatus = EnemyStatus.Dead;
        StopMoving();
        animator.SetBool("isDie", true);
        animator.Play("isDead");

        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(this);
        }
        
        GameObject coin = Instantiate(coinPrefab, coinPivot.position, Quaternion.identity);
        coin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), Random.Range(-10f, 10f)));
        coin.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), Random.Range(-10f, 10f)));
        Destroy(gameObject, 1f);
        gm.deadEnemyCounter++;
    }
}