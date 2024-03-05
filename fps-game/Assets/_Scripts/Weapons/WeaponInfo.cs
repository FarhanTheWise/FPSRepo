using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Weapon Info", fileName = "Weapon")]
public class WeaponInfo : ScriptableObject
{
    [Header("Basic Weapon States")]
    public WeaponType weaponType;
    public string weaponName;
    public float rateOfFire;
    public int maxMagazineSize;
    public float baseDamage;
    public float bulletForce;
    public float reloadTime;

    //procedural weapon stats
    [Header("Procedural Recoil")]

    //Hip Fire
    public float xRecoilRot;
    public float yRecoilRot;
    public float zRecoilRot;

    [Space()]

    //aim Fire
    public float xAimRecoilRot;
    public float yAimRecoilRot;
    public float zAimRecoilRot;

    [Space()]

    //procedural spreed
    [Header("Procedural Recoil")]
    public float spreadAmount;

    //procedural kick

    public enum WeaponType
    {
        SemiAutomatic = 0,
        Automatic = 1,
        Shotgun = 2
    }

}
