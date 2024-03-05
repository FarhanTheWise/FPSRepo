using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAutomatic : Weapon
{

    private float _currentBulletDelay = 0;
    public float _currentReloadDelay = 0;

    private void Update()
    {
        if (isShooting && currentBullets > 0)
        {
            OnShootWeapon();
        }
  
        if(currentBullets <= 0)
        {
            isShootable = false;
            isReloading = true;
            OnReloadWeapon();
        }
    }

    public override void OnShootWeapon()
    {

        if (isShootable)
        {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 directionWithoutSpread = targetPoint - muzzle.transform.position;

            float x = Random.Range(-weaponInfo.spreadAmount, weaponInfo.spreadAmount);
            float y = Random.Range(-weaponInfo.spreadAmount, weaponInfo.spreadAmount);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            PoolingManager.Instance.SpawnObjectBullet(muzzle.transform.position, muzzle.transform.rotation, PoolName.Bullet, weaponInfo.bulletForce, directionWithSpread);
            currentBullets--;
            isShootable = false;
        }

       
        _currentBulletDelay += Time.deltaTime;
        if(_currentBulletDelay >= rateOfFire)
        {
            _currentBulletDelay = 0;
            isShootable = true;
        }

        
    }

    public override void OnReloadWeapon()
    {
        _currentReloadDelay += Time.deltaTime;
        if(_currentReloadDelay >= weaponInfo.reloadTime)
        {
            _currentReloadDelay = 0;
            currentBullets = totalMagSize;
            isShootable = true;
            isReloading = false;
        }
    }

}
