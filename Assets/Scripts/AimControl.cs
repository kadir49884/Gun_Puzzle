using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AimControl : MonoBehaviour
{
    [SerializeField]
    private Settings settings;
    private float _tempX;
    private float _tempZ;

    private Vector3 _firstPos;
    private Vector3 _lastPos;
    private Vector3 _differentPos;
    private bool _checkFirstShoot;
    private GameManager gameManager;
    private ObjectManager objectManager;
    private Camera camera;


    private void Start()
    {
        objectManager = ObjectManager.Instance;
        gameManager = GameManager.Instance;
        camera = objectManager.CameraObject;
    }

    void Update()
    {
        if (gameManager.CurrentState == State.Shoot)
        {
            if (Input.GetMouseButtonDown(0))
            {
                FirstTouchControl();
            }
            else if (Input.GetMouseButton(0))
            {
                if (!_checkFirstShoot)
                {
                    FirstTouchControl();
                    _checkFirstShoot = true;
                }
                OnMouseDown();
            }
        }
    }

    private void FirstTouchControl()
    {
        Vector3 screenToWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        _firstPos = new Vector3(_firstPos.x, screenToWorld.x, screenToWorld.y);
        _tempX = transform.eulerAngles.y;
        _tempZ = transform.eulerAngles.z;
    }

    private void OnMouseDown()
    {
        Vector3 screenToWorld = camera.ScreenToWorldPoint(Input.mousePosition);
        _lastPos = new Vector3(_lastPos.x, screenToWorld.x, screenToWorld.y);
        _differentPos = new Vector3(transform.eulerAngles.x,
            (_tempX + (_lastPos.y - _firstPos.y) * settings.AimSpeed),
            (_tempZ + (_lastPos.z - _firstPos.z) * settings.AimSpeed));

        _differentPos.y = Mathf.Clamp(_differentPos.y, settings.AimIntervalYmin, settings.AimIntervalYmax);
        _differentPos.z = Mathf.Clamp(_differentPos.z, settings.AimIntervalZmin, settings.AimIntervalZmax);
        transform.DORotate(_differentPos, 0.1f, RotateMode.Fast);
    }

}
