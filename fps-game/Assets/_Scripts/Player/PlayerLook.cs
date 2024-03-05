using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float turnSpeed = 15f;
    [SerializeField] private Transform cameraLookAt;

    private Camera mainCamera;
    public Transform cameraVerticalPivot;
    public GameObject virtualCameraObject;
    private CinemachineVirtualCamera virtualCamera;

    [Range(0, 10)] public float sensitivity;

    private void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;

        virtualCamera = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivity * 100f;
        virtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivity * 100f;

    }

    private void LateUpdate()
    {
        //virtual camera sensitivity
        
        
        //horizontal camera
        var yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
        
        //vertical camera
        var verticalMovement = mainCamera.transform.rotation.eulerAngles.x;
        cameraVerticalPivot.rotation = Quaternion.Slerp(cameraVerticalPivot.rotation, Quaternion.Euler(verticalMovement, yawCamera, 0), turnSpeed * Time.deltaTime);
        //cameraVerticalPivot.rotation = Quaternion.Euler(verticalMovement, yawCamera, 0);


    }
}
