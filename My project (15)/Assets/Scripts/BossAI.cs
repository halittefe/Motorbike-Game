using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    //NAVMESH VE HASAR TASARIMINI GUNCELLE
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinPivot;
    public Transform AimPivot;
    public float minDistance;
    public float chaseDistance = 20f;
    public float attackDistance = 5f;
    public int bossDamage = 35;
    public Animator animator;
    public bool isDead = false;
    private bool isAttacking;
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    public bool isShoot = false;
    private Vector3 lastPosition;
    PlayerController pc;
    PanelManager pm;
    Collider Playercollider;

    CameraShake cameraShake;
    private void Start()
    {
        currentHealth = maxHealth;
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        pm = GameObject.Find("GameManager").GetComponent<PanelManager>();
        cameraShake = GameObject.Find("Camera").GetComponent<CameraShake>();
        Playercollider = GameObject.Find("Player").GetComponent<BoxCollider>();
    }
    private void Update()
    {
        //distance = transform.pos - hedef
        /*
        if (currentHealth <= 0)
        {
            isDead = true;
            Die();
            cameraShake.shakeDuration = 0;
            Invoke("NextPanel", 5f);
            return;
        }
        */

        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, AimPivot.position);

        if (distanceToPlayer <= chaseDistance /*&& distanceToPlayer >=minDistance*/ )
        {
            if (distanceToPlayer <= attackDistance)
            {
                //animator.SetBool("isWalking", true );

                animator.SetBool("isShoot", true);
                Playercollider.isTrigger = true; 
                isShoot = true;
                transform.position += transform.forward * Time.deltaTime;

            }
            else
            {
                animator.SetBool("isWalking", true);
                animator.SetBool("isShoot", false);
                transform.LookAt(AimPivot);
                transform.position += transform.forward * Time.deltaTime * 5f; // BossEnemy'nin h�z�
                isShoot = false;
                Playercollider.isTrigger = false;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isShoot", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && animator.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
        {
            Debug.Log("damage");
            pc.currentHealth -= bossDamage;
            pc.HealthBar();
            if (pc.currentHealth <= 0)
            {
                cameraShake.shakeDuration = 0;
                pc.Die();
            }
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }
    public void Die()
    { 
        for (int i = 0; i < 5; i++)
        {
            GameObject coin = Instantiate(coinPrefab, coinPivot.position, Quaternion.identity);
            coin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), Random.Range(-10f, 10f)));
            coin.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-10f, 10f), Random.Range(5f, 10f), Random.Range(-10f, 10f)));
        }
        Destroy(gameObject, 20f);
        pm.nextPanel.SetActive(true);
        pm.ParticleEffect.SetActive(true);
        animator.SetBool("isDie", true);
        animator.SetBool("isShoot", false);
        animator.SetBool("isWalking", false);
    }

    void NextPanel()
    {
        pm.nextPanel.SetActive(true);
        pm.ParticleEffect.SetActive(true);
    }

}
