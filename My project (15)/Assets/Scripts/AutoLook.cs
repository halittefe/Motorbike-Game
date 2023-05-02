using System.Linq;
using UnityEngine;

public class AutoLook : MonoBehaviour
{
    [Header("For Aim")]
    public GameObject fovStartPoint;
    public float raycastMax = 100f;
    public float lookSpeed = 200f;
    public float maxAngle = 90;
    public float aimRange = 20;
    public LayerMask obstacleLayer;
    public Quaternion ResetRotation;
    private Quaternion targetRotation;
    private Quaternion lookAt;
    private Transform closestEnemy;
    private EnemyAI enemyScript;
    private GameObject[] enemies;

    private void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        closestEnemy?.TryGetComponent(out enemyScript);
    }

    private void Update()
    {
        if (enemies == null || !enemies.Any()) return;

        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue;

            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= aimRange)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
                closestEnemy?.TryGetComponent(out enemyScript);
            }
        }

        if (closestEnemy == null) return;

        if (EnemyInFieldOfView(fovStartPoint))
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, raycastMax)) //, obstacleLayer))
            {
                targetRotation = hit.collider.tag != "Building" ? Quaternion.LookRotation(direction) : enemyScript.IsDead ? ResetRotation : targetRotation;
                lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * lookSpeed);
                transform.rotation = lookAt;
            }
        }
        else
        {
            targetRotation = ResetRotation;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * lookSpeed);
        }
    }

    private bool EnemyInFieldOfView(GameObject looker)
    {
        if (closestEnemy == null) return false;

        Vector3 targetDir = closestEnemy.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, looker.transform.forward);
        return angle <= maxAngle;
    }
}
