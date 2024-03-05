using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{

    public bool isHeadbobEnabled;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float _frequency = 10f;
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private float speed;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();      
    }

    private void Update()
    {
        if (!isHeadbobEnabled) return;
        
        CheckMotion();
    }

    void GenerateHeadBob()
    {
        noise.m_AmplitudeGain = _amplitude * speed;
        noise.m_FrequencyGain = _frequency * speed;
    }

    private void CheckMotion()
    {
        speed = new Vector3(playerMovement._move.x, 0, playerMovement._move.z).magnitude;
        if (!playerMovement.isGrounded) return;
        GenerateHeadBob();
    }

}
