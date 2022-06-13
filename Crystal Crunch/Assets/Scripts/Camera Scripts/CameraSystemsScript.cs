using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public enum CameraShakeType
{
    playerHit,
    playerLightAttack,
    playerHeavyAttack,
    playerLeap
}

public class CameraSystemsScript : MonoBehaviour
{

    [SerializeField] VolumeProfile DefaultVP;

    [SerializeField] areaCameraProperties CurrentCameraSettings;
    [SerializeField] areaCameraProperties DefaultCameraSettings;

    bool Transitioning;

    [SerializeField] GameObject Camera;
    [SerializeField] Camera BlurEffectCamera;
    [SerializeField] float Speed = 5f;

    Vector3 LerpTarget;
    [SerializeField] GameObject PlayerTarget;

    [SerializeField] Cinemachine.CinemachineVirtualCamera VirtualCam;


    [Header("Camera Impulse Source Reference")]
    [SerializeField] Cinemachine.CinemachineImpulseSource ImpulseSource;
    [Header("Camera Shake Profiles")]
    [SerializeField] Cinemachine.NoiseSettings PlayerHitNoiseSettings;
    [SerializeField] Cinemachine.NoiseSettings PlayerLeapNoiseSettings;
    [SerializeField] Cinemachine.NoiseSettings PlayerLightAttackNoiseSettings;
    [SerializeField] Cinemachine.NoiseSettings PlayerHeavyAttackNoiseSettings;

    private void Start()
    {
        CurrentCameraSettings = DefaultCameraSettings;
        PlayerTarget = FindObjectOfType<PlayerStats>().gameObject; 
        LerpTarget = PlayerTarget.transform.position;
        #region Getting Camera
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).gameObject.GetComponent<Camera>())
            {
                Camera = this.transform.GetChild(i).gameObject;
            }
        }
        #endregion
       
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.OnChangeCamera += ChangeCameraEvent;
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.RestoreDefault += DefaultCamera;
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.OnCameraShake += ShakeCamera;

        GameEventsSystem.gameEventsSystem.CameraEventsSystem.ChangePostProcessingProfileEvent += ChangeCameraProfile;
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.RestoreDefaultPPEvent += RestoreDefaultPP;
    }






   /* private void Update()
    {
        UpdateCamera();
        LerpToTarget(CurrentCameraSettings.TransitionSpeed);
    }*/

    private void FixedUpdate()
    {
        UpdateCamera();
        LerpToTarget(CurrentCameraSettings.TransitionSpeed);
    }

    void ChangeCameraProfile(VolumeProfile VP)
    {

        if(VirtualCam.gameObject!=null)
        {
            VirtualCam.gameObject.GetComponent<CinemachineVolumeSettings>().m_Profile = VP;
        }

    }

    void RestoreDefaultPP()
    {
        if (VirtualCam.gameObject != null)
        {
            VirtualCam.gameObject.GetComponent<CinemachineVolumeSettings>().m_Profile = DefaultVP;
        }
    }

    IEnumerator SwappingProfiles(VolumeProfile VP)
    {
        yield break;

    }

    void ShakeCamera(CameraShakeType ShakeType, float ShakeDuration, Cinemachine.NoiseSettings Settings)
    {
        
        if (ImpulseSource == null)
            return;

        if (Settings != null)
        {
            ImpulseSource.m_ImpulseDefinition.m_RawSignal = Settings;
            ImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = ShakeDuration;
            ImpulseSource.GenerateImpulse();
            return;
        }
        switch (ShakeType)
        {
            case CameraShakeType.playerLightAttack:
                ImpulseSource.m_ImpulseDefinition.m_RawSignal = PlayerLightAttackNoiseSettings;
                break;
            case CameraShakeType.playerHit:
                ImpulseSource.m_ImpulseDefinition.m_RawSignal = PlayerHitNoiseSettings;
                break;
            case CameraShakeType.playerHeavyAttack:
                ImpulseSource.m_ImpulseDefinition.m_RawSignal = PlayerHeavyAttackNoiseSettings;
                break;
            case CameraShakeType.playerLeap:
                ImpulseSource.m_ImpulseDefinition.m_RawSignal = PlayerLeapNoiseSettings;
                break;
        }
        ImpulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = ShakeDuration;
        ImpulseSource.GenerateImpulse();
    }

    private void UpdateCamera()
    {
        /*
        Camera.GetComponent<Camera>().orthographicSize = Mathf.Lerp(Camera.GetComponent<Camera>().orthographicSize, 
                                                                        CurrentCameraSettings.CameraSize, 
                                                                        Time.deltaTime*Speed);
        */
        VirtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Lerp(VirtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize,
                                                                                                CurrentCameraSettings.CameraSize,
                                                                                                Time.deltaTime * Speed
                                                                                                );

        BlurEffectCamera.orthographicSize = VirtualCam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;

        if (Transitioning)
        {
            switch(CurrentCameraSettings.myType)
            {
                case TrackerType.Fixed:
                    LerpTarget = CurrentCameraSettings.positionCenter;
                    break;
                case TrackerType.XTracker:
                    float DistY = (PlayerTarget.transform.position.y - CurrentCameraSettings.positionCenter.y);
                    LerpTarget = new Vector3(PlayerTarget.transform.position.x, CurrentCameraSettings.positionCenter.y + (DistY/2) + (DistY * CurrentCameraSettings.CompositeWeight));
                    break;
                case TrackerType.YTracker:
                    float DistX = (PlayerTarget.transform.position.x - CurrentCameraSettings.positionCenter.x);
                    LerpTarget = new Vector2(CurrentCameraSettings.positionCenter.x + (DistX / 2) + (DistX * CurrentCameraSettings.CompositeWeight),  PlayerTarget.transform.position.y);
                    break;
                case TrackerType.FullTracker:
                    LerpTarget = PlayerTarget.transform.position;
                    break;
                case TrackerType.FullTrackFixedComposite:
                    Vector3 Dir = (PlayerTarget.transform.position - CurrentCameraSettings.positionCenter);
                    Vector3 MidPoint = CurrentCameraSettings.positionCenter + ((PlayerTarget.transform.position - CurrentCameraSettings.positionCenter) / 2) + (Dir*CurrentCameraSettings.CompositeWeight);
                    LerpTarget = MidPoint;
                    break;


            }
            if (Vector3.Distance(LerpTarget, this.transform.position) < 1f)
                Transitioning = false;
        }
        else
        {
            CurrentCameraSettings.TransitionSpeed = DefaultCameraSettings.TransitionSpeed;
            switch(CurrentCameraSettings.myType)
            {
                case TrackerType.zero:
                    LerpTarget = PlayerTarget.transform.position;
                    break;
                case TrackerType.FullTracker:
                    LerpTarget = PlayerTarget.transform.position;
                    break;
                case TrackerType.XTracker:
                    LerpTarget = new Vector2(PlayerTarget.transform.position.x, 
                                                this.transform.position.y);                    
                    break;
                case TrackerType.YTracker:
                    LerpTarget = new Vector2(this.transform.position.x, 
                                            PlayerTarget.transform.position.y);
                    break;
                case TrackerType.Fixed:
                    LerpTarget = CurrentCameraSettings.positionCenter;
                    break;
            }

        }
    }

    void LerpToTarget(float speed)
    {
        this.transform.position = Vector2.Lerp(this.transform.position, LerpTarget, speed*Time.deltaTime);
    }

    void ChangeCameraEvent(areaCameraProperties myProps, bool isTransitioning)
    {
        CurrentCameraSettings = myProps;
        Transitioning = isTransitioning;
    }

    void DefaultCamera()
    {
        CurrentCameraSettings = DefaultCameraSettings;
    }
    
    void SetSpeed(float nuSpeed)
    {
        Speed = nuSpeed;
    }

}
