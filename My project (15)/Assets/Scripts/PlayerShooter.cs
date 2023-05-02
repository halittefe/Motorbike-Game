using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private float bulletHitRadius = 10f;
    public int bulletsLeft, bulletsShot;
    public bool gunChanged;
    public bool allowInvoke = true;
    public bool shooting, readyToShoot, reloading;
    public Transform attackPoint;
    public GameObject muzzleFlash;
    private BulletPooler bulletPooler;
    private GameManager gm;

    [Header("For Pistol")]
    public float shootForcePistol, upwardForcePistol;
    public float timeBetweenShootingPistol, spreadPistol, reloadTimePistol; //, timeBetweenShotsPistol;
    public int bulletDamageAmountPistol;
    public int magazineSizePistol;

    [Header("For Smg")]
    public float shootForceSmg, upwardForceSmg;
    public float timeBetweenShootingSmg, spreadSmg, reloadTimeSmg; //, timeBetweenShotsPistol;
    public int bulletDamageAmountSmg;
    public int magazineSizeSmg;

    [Header("For Revolver")]
    public float shootForceRevolver, upwardForceRevolver;
    public float timeBetweenShootingRevolver, spreadRevolver, reloadTimeRevolver; //, timeBetweenShotsPistol;
    public int bulletDamageAmountRevolver;
    public int magazineSizeRevolver;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        bulletPooler = GameObject.Find("Pooler").GetComponent<BulletPooler>();
        //MAGAZINE SIZE, MAKE ALL OF THEM WORK WITH GUN TYPE
        if (gm.isPistol)
        {
            bulletsLeft = magazineSizePistol;
        }
        if (gm.isSmg)
        {
            bulletsLeft = magazineSizeSmg;
        }
        if (gm.isRevolver)
        {
            bulletsLeft = magazineSizeRevolver;
        }
        //bulletsLeft = magazineSizePistol;
        readyToShoot = true;
    }

    public void ShootSystem(Vector3 target)
    {
        /*
         * if(gunchanged)
         * change bulletamount to changed gun.
         */
        //for pistol
        if (gm.isPistol && !gm.isSmg && !gm.isRevolver)
        {
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                Shoot(target, shootForcePistol, upwardForcePistol, timeBetweenShootingPistol, spreadPistol, bulletDamageAmountPistol);
            }
            else if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) { Reload(reloadTimePistol); }
        }
        //for SMG
        if (gm.isSmg && !gm.isPistol && !gm.isRevolver)
        {
            if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            {
                Shoot(target, shootForceSmg, upwardForceSmg, timeBetweenShootingSmg, spreadSmg, bulletDamageAmountSmg);
            }
            else if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) { Reload(reloadTimeSmg); }
        }
        //for Revolver
        if (gm.isRevolver && !gm.isPistol && !gm.isSmg)
        {
            if(readyToShoot && shooting && !reloading &&bulletsLeft > 0)
            {
                Shoot(target, shootForceRevolver, upwardForceRevolver, timeBetweenShootingRevolver, spreadRevolver, bulletDamageAmountRevolver);
            }
            else if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) { Reload(reloadTimeRevolver); }

        }
    }

    private void Shoot(Vector3 targetPosition, float shootForce, float upwardForce, float timeBetweenShooting, float spread, int bulletDamageAmount)
    {
        readyToShoot = false;
        Vector3 directionWithoutSpread = targetPosition - transform.position;
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        // Get a bullet from the object pool
        GameObject currentBullet = bulletPooler.GetPlayerBullet();
        currentBullet.transform.position = attackPoint.position;
        currentBullet.transform.rotation = attackPoint.rotation;
        currentBullet.SetActive(true);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(transform.up * upwardForce, ForceMode.Impulse);
        //currentBullet.GetComponent<PlayerBullet>().damageAmount = bulletDamageAmount;

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        shooting = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    public void Reload(float reloadTime)
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }

    private void ReloadFinished()
    {
        //MAGAZINE SIZE, MAKE ALL OF THEM WORK WITH GUN TYPE
        if (gm.isPistol && !gm.isSmg && !gm.isRevolver) bulletsLeft = magazineSizePistol;
        if (!gm.isPistol && gm.isSmg && !gm.isRevolver) bulletsLeft = magazineSizeSmg;
        if (!gm.isPistol && !gm.isSmg && gm.isRevolver) bulletsLeft = magazineSizeRevolver;
        reloading = false;
    }
}
