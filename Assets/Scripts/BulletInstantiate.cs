using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiate : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletObject;

    [SerializeField]
    private GameObject gunBarrelObject;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    private Settings settings;

    private bool _isReady = true;

    private GameManager gameManager;
    private CameraControl cameraControl;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cameraControl = CameraControl.Instance;
    }

    private void Update()
    {
        if (gameManager.CurrentState == State.Shoot && _isReady)
        {
#if UNITY_EDITOR 
            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(bulletObject, gunBarrelObject.transform.position, gunBarrelObject.transform.rotation, gunBarrelObject.transform);
                TargetControl.Instance.ReduceBullet();
                cameraControl.CameraShakePos();
                muzzleFlash.Play();
                _isReady = false;
                Invoke("WaitForShootTime", settings.ShoottingTime);
            }

#elif  UNITY_ANDROID
            if (Input.touches.Length != 0)
            {
                if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    Instantiate(bulletObject, gunBarrelObject.transform.position, gunBarrelObject.transform.rotation, gunBarrelObject.transform);
                    TargetControl.Instance.ReduceBullet();
                    cameraControl.CameraShakePos();
                    muzzleFlash.Play();
                    _isReady = false;
                    Invoke("WaitForShootTime", settings.ShoottingTime);
                }
            }
#endif
        }
    }
    private void WaitForShootTime()
    {
        _isReady = true;
    }
}
