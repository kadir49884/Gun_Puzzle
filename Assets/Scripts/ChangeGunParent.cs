using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunParent : MonoBehaviour
{

    [SerializeField]
    private GameObject gunNewPos;

    [SerializeField]
    private GameObject crosshair;

    private CameraControl cameraControl;


    private static ChangeGunParent instance = null;
    public static ChangeGunParent Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        cameraControl = CameraControl.Instance;
    }

    public void SetGunNewPos()
    {
        cameraControl.CameraShootPos();
        Invoke("SetGunNewParent", 3.2f);
    }

    private void SetGunNewParent()
    {
        if(transform.childCount > 0)
        {
            transform.GetChild(0).parent = gunNewPos.transform;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).parent = gunNewPos.transform; // I am aware
            }
        }
        Invoke("ShowCrosshair", 1.5f);
        cameraControl.CameraNewPos();
    }

    private void ShowCrosshair()
    {
        BaseControl.Instance.NewParentForRotate();
        crosshair.SetActive(true);
        GameManager.Instance.CurrentState++;
        gunNewPos.GetComponent<AimControl>().enabled = true;
    }
}
