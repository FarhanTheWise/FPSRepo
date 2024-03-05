using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 15f;
    private Camera _mainCamera;
    public Transform cameraVerticalPivot;
    public GameObject virtualCameraObject;
    private CinemachineVirtualCamera _virtualCamera;

    [Range(0, 10)] public float sensitivity;

    private void Start()
    {
        _mainCamera = Camera.main;
        Cursor.visible = false;

        _virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivity * 100f;
        _virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivity * 100f;

    }

    private void LateUpdate()
    {
        //horizontal camera
        var yawCamera = _mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
        
        //vertical camera
        var verticalMovement = _mainCamera.transform.rotation.eulerAngles.x;
        cameraVerticalPivot.rotation = Quaternion.Slerp(cameraVerticalPivot.rotation, Quaternion.Euler(verticalMovement, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}
