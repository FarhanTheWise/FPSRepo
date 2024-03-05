using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : MonoBehaviour
{

    public bool isShooting;
    public bool isReloading;
    public bool isShootable;
    public WeaponInfo weaponInfo;
    public WeaponInfo.WeaponType weaponType;
    public GameObject muzzle;
    public CustomInputs inputs;
    public float rateOfFire;

    [SerializeField] 
    protected float currentBullets;
    protected float totalMagSize;

    private void Awake()
    {
        inputs = new CustomInputs();
    }

    private void OnEnable()
    {
     
        inputs.Enable();
        inputs.Weapon.Shoot.performed += context => ShootingInput(true);
        inputs.Weapon.Shoot.canceled += context => ShootingInput(false);
   
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void Start()
    {
        isShootable = true;    
        weaponType = weaponInfo.weaponType;
        currentBullets = totalMagSize = weaponInfo.maxMagazineSize;
        rateOfFire = weaponInfo.rateOfFire;
    }

    void ShootingInput(bool result)
    {
        isShooting = result;
    }

    public abstract void OnShootWeapon();
    public abstract void OnReloadWeapon();
}
