using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public GameObject fovStartPoint;
    public float raycastMax = 1000f;
    public float lookSpeed = 200f;
    public float maxAngle = 90;
    public float aimRange = 20;
    public LayerMask obstacleLayer;

    private Quaternion targetRotation;
    private Transform closestEnemy;
    private EnemyAI enemyScript;
    private float rotationSmoothing = 5f;
    private float timeToFire;
    private List<Transform> detectedEnemies = new List<Transform>();
    private int currentTargetIndex = -1;
    private PlayerShooter playerShooter;

    private void Start()
    {
        playerShooter = GetComponent<PlayerShooter>();
    }

    private void Update()
    {
        UpdateTarget();
        UpdateRotation();
        ShootIfPossible();
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= aimRange)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }

            else if (!detectedEnemies.Contains(enemy.transform) && distance <= aimRange)
            {
                detectedEnemies.Add(enemy.transform);
            }
        }

        if (closestEnemy != null)
        {
            EnemyAI enemyHealth = closestEnemy.GetComponent<EnemyAI>();
            if (enemyHealth != null && enemyHealth.currentHealth <= 0)
            {
                closestEnemy = null;
            }
        }

        if (closestEnemy == null && detectedEnemies.Count > 0)
        {
            closestEnemy = detectedEnemies[0];
            if (closestEnemy != null)
            {
                EnemyAI enemyHealth = closestEnemy.GetComponent<EnemyAI>();
                if (enemyHealth != null)
                {
                    if (enemyHealth.currentHealth <= 0)
                    {
                        detectedEnemies.RemoveAt(0);
                        closestEnemy = null;
                    }
                }
            }
        }
    }

    private void UpdateRotation()
    {
        if (closestEnemy != null)
        {
            enemyScript = closestEnemy.GetComponent<EnemyAI>();
        }

        if (EnemyInFieldOfView())
        {
            if (closestEnemy == null)
            {
                ResetTargetRotation();
                return;
            }
            Vector3 direction = closestEnemy.transform.position - transform.position;
            // Create a layer mask that includes the "Enemy" layer
            int layerMask = 1 << LayerMask.NameToLayer("Enemy");
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, raycastMax, layerMask))
            {
                targetRotation = Quaternion.LookRotation(direction);
            }
            else
            {
                ResetTargetRotation();
            }
        }
        else
        {
            ResetTargetRotation();
        }

        Quaternion maxRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, maxRotation, Time.deltaTime * rotationSmoothing);
    }


    private void ResetTargetRotation()
    {
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private bool EnemyInFieldOfView()
    {
        if (closestEnemy == null) return false;

        Vector3 targetDir = closestEnemy.position - transform.position;
        float angle = Vector3.Angle(targetDir, fovStartPoint.transform.forward);
        return angle <= maxAngle;
    }

    private void ShootIfPossible()
    {
        if (closestEnemy != null && EnemyInFieldOfView())
        {
            Transform aimPivot = closestEnemy.Find("AimPivot");
            if (aimPivot != null && aimPivot.position != null)
            {
                Vector3 targetPosition = aimPivot.position;
                playerShooter.shooting = true;
                playerShooter.ShootSystem(targetPosition);
            }
            //Reloading 
            //3 saniye boyunca herhangi bir yere ates etmezse reload if blogu
            //else if (playerShooter.readyToShoot && playerShooter.shooting && !playerShooter.reloading && playerShooter.bulletsLeft <= 0) { playerShooter.Reload(); }
        }
    }
}
