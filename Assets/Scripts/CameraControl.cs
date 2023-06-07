using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraControl : MonoBehaviour
{

    private static CameraControl instance = null;
    public static CameraControl Instance { get => instance; set => instance = value; }

    private Vector3 _cameraShakePos;

    [SerializeField]
    private GameObject gunObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CameraShootPos()
    {
        transform.DOMove(gunObject.transform.GetChild(1).transform.position, 3f);
        transform.DOLocalRotate(gunObject.transform.GetChild(1).localEulerAngles, 3f, RotateMode.Fast);
        GetComponent<Camera>().DOFieldOfView(60, 2f);
    }

    public void CameraNewPos()
    {
        transform.DOMove(gunObject.transform.GetChild(1).transform.position, 1f);
        transform.DOLocalRotate(gunObject.transform.GetChild(1).localEulerAngles, 1f, RotateMode.Fast);
    }


    public void CameraShakePos()
    {
        _cameraShakePos = transform.localEulerAngles;
        transform.DOLocalRotate(new Vector3(_cameraShakePos.x - 1f, _cameraShakePos.y - 1f, _cameraShakePos.z - 1f), 0.08f, RotateMode.Fast);
        Invoke("CameraShakePosBack", 0.1f);
    }
    private void CameraShakePosBack()
    {
        transform.DOLocalRotate(_cameraShakePos, 0.08f, RotateMode.Fast);
    }


}
