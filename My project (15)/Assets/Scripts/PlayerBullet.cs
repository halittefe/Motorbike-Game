using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private PlayerShooter shoot;
    private GameManager gm;
    private void Start()
    {
        shoot = GameObject.Find("LeftArmPivot").GetComponent<PlayerShooter>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void OnTriggerEnter(Collider coll)
{
    if (coll.CompareTag("Enemy"))
    {
        EnemyAI enemyAI = coll.GetComponent<EnemyAI>();
        BossAI bossAI = coll.GetComponent<BossAI>(); // check if it is a boss
        if (bossAI != null)
        {
            if (gm.isPistol)
                bossAI.TakeDamage(shoot.bulletDamageAmountPistol);
            else if (gm.isSmg)
                bossAI.TakeDamage(shoot.bulletDamageAmountSmg);
            else if (gm.isRevolver)
                bossAI.TakeDamage(shoot.bulletDamageAmountRevolver);
        }
        else
        {
            if (gm.isPistol)
                enemyAI.TakeDamage(shoot.bulletDamageAmountPistol);
            else if (gm.isSmg)
                enemyAI.TakeDamage(shoot.bulletDamageAmountSmg);
            else if (gm.isRevolver)
                enemyAI.TakeDamage(shoot.bulletDamageAmountRevolver);
        }
    }
}

}
