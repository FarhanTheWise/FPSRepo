using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimations : MonoBehaviour
{

    public PlayerMovement playerMovement;
    
    //Weapon Idle Sway

    [Serializable]
    public class WeaponIdleSwayClass
    {
        public bool isSway;
        public Transform swayObject;
        public float swayAmountA = 1;
        public float swayAmountB = 2;
        public float swayScale = 100;
        public float swayLerpSpeed = 14;
        public float resetSpeed = 1000;

    }

    [Serializable]
    public class WeaponRecoilClass
    {
        public bool isRecoil;
        public Transform swayObject;
        public float swayAmountA = 1;
        public float swayAmountB = 2;
        public float swayScale = 100;
        public float swayLerpSpeed = 14;
        public float resetSpeed = 1000;

    }
    
    [Header("Procedural Weapon Sway")] 
    public WeaponIdleSwayClass weaponIdleSway;
    private Vector3 _startPosition;
    private float _swayTime;
    private float _swayTotalTime;
    private Vector3 _swayPosition;

    [Space()]
    
    [Header("Procedural Weapon Sway")] 
    public WeaponRecoilClass weaponRecoil;
    
    private void Start()
    {
        _swayTime = 0;
        _swayTotalTime = Mathf.Round((Mathf.PI * 2) * 10.0f) * 0.1f;;
        _startPosition = Vector3.zero;
    }

    void CalculateWeaponSway()
    {
        var targetPosition = LissaJousCurve(_swayTime, weaponIdleSway.swayAmountA, 
            weaponIdleSway.swayAmountB) / weaponIdleSway.swayScale;
        _swayTime += Time.deltaTime;
        if (_swayTime >= _swayTotalTime)
        {
            _swayTime = 0;
        }

        _swayPosition = Vector3.Lerp(_swayPosition, targetPosition, 
            weaponIdleSway.swayLerpSpeed * Time.smoothDeltaTime);
        weaponIdleSway.swayObject.localPosition = _swayPosition;
    }
    
    Vector3 LissaJousCurve(float time, float a, float b)
    {
        return new Vector3(Mathf.Sin(time), a * Mathf.Sin(b * time + Mathf.PI));
    }

    void ResetPosition()
    {
        var resetPos = Vector3.Lerp(_swayPosition, _startPosition, 
            weaponIdleSway.resetSpeed * Time.deltaTime);
        weaponIdleSway.swayObject.localPosition = resetPos;
    }
    
    private void Update()
    {
        if (!weaponIdleSway.isSway) return;
        if (playerMovement.isMoving)
        {
            ResetPosition();
        }
        else
        {
            CalculateWeaponSway();
        }
       
    }
}
