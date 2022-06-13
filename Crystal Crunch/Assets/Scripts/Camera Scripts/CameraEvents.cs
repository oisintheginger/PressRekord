using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
public class CameraEvents : MonoBehaviour
{
    

    public event Action< areaCameraProperties, bool>  OnChangeCamera;

    public event Action RestoreDefault;
    public void ChangeCamera(areaCameraProperties newProps, bool isTransitioning)
    {
        if (OnChangeCamera != null)
        {
            OnChangeCamera.Invoke(newProps, isTransitioning);
        }
    }
    public void RestoreDefaultCamera()
    {
        if(RestoreDefault!=null)
        {
            RestoreDefault.Invoke();
        }
    }


    public event Action<CameraShakeType, float, Cinemachine.NoiseSettings> OnCameraShake;
    public void CameraShake(CameraShakeType ShakeType, float Duration, Cinemachine.NoiseSettings SpecialNoiseSettings = null)
    {
        if(OnCameraShake!=null)
        {
            OnCameraShake.Invoke(ShakeType, Duration, SpecialNoiseSettings);
        }
    }


    public event Action<VolumeProfile> ChangePostProcessingProfileEvent;

    public void OnChangePostProcessingProfile(VolumeProfile VP)
    {
        if(ChangePostProcessingProfileEvent!=null)
        {
            ChangePostProcessingProfileEvent.Invoke(VP);
        }
    }
    public event Action RestoreDefaultPPEvent;

    public void OnRestoreDefaultPP()
    {
        if (RestoreDefaultPPEvent != null)
        {
            RestoreDefaultPPEvent.Invoke();
        }
    }


    public event Action<GameObject> SwapVirtualCameraEvent;
    public void OnSwapVirtualCamera(GameObject TriggerObject)
    {
        if(SwapVirtualCameraEvent!=null)
        {
            SwapVirtualCameraEvent.Invoke(TriggerObject);
        }
    }
}
