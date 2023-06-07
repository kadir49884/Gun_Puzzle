using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BaseControl : MonoBehaviour
{

    [SerializeField]
    private GameObject baseObject;
    [SerializeField]
    private GameObject magazineObject;
    [SerializeField]
    private GameObject slideObject;
    [SerializeField]
    private GameObject gunPieceParent;
    [SerializeField]
    private Transform magazineNewPos;
    [SerializeField]
    private Transform magazineInBaseNewPos;
    [SerializeField]
    private Transform baseNewPos;
    [SerializeField]
    private Transform slideNewPos;
    [SerializeField]
    private Transform slideInBasePos;


    private GameObject gunObject;
    private static BaseControl instance = null;
    public static BaseControl Instance { get => instance; set => instance = value; }
    GameManager gameManager;
    private Vector3 _gunUpPos;
    private Vector3 _gunShakePos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        gunObject = transform.parent.gameObject;
        _gunUpPos = new Vector3(0, 180, 0);
        _gunShakePos = new Vector3(-10f, 0, 0);
    }
    public void RotateAndTransformBase()
    {
        magazineObject.transform.DOLocalRotate(magazineNewPos.localEulerAngles, 1, RotateMode.Fast);
        baseObject.transform.DOLocalRotate(baseNewPos.localEulerAngles, 1, RotateMode.Fast);
        baseObject.transform.DOLocalMove(baseNewPos.localPosition, 0.5f);
        Invoke("MagazineGoBaseTip", 1f);
    }
    private void MagazineGoBaseTip()
    {
        magazineObject.transform.DOLocalMove(magazineNewPos.localPosition, 0.5f);
    }
    public void MagazineGoBase()
    {
        magazineObject.transform.DOLocalMove(magazineInBaseNewPos.localPosition, 0.5f);
        Invoke("SlideGoInBaseLate", 1f);
    }
    private void SlideGoInBaseLate()
    {
        slideObject.transform.DOLocalRotate(slideNewPos.localEulerAngles, 1, RotateMode.Fast);
        slideObject.transform.DOLocalMove(slideNewPos.localPosition, 0.7f);
        Invoke("SlideWaitForStateUp", 1f);
    }
    private void SlideWaitForStateUp()
    {
        gameManager.CurrentState++;
    }
    public void SlideGoInBase()
    {
        slideObject.transform.DOLocalMove(slideInBasePos.localPosition, 0.5f);
        Invoke("GunUp", 1f);
        Invoke("SetGunNewPosition", 3f);
        Invoke("RotateAndTransformGun", 6f);
    }
    private void GunUp()
    {
        transform.parent.DOLocalRotate(_gunUpPos, 1, RotateMode.Fast);
        transform.parent.transform.DOMoveY(0.15f, 1.5f);
    }
    private void RotateAndTransformGun()
    {
        transform.DOLocalMove(gunObject.transform.GetChild(2).localPosition, 1f);
        transform.DOLocalRotate(gunObject.transform.GetChild(2).localEulerAngles, 1, RotateMode.Fast);
    }
    private void SetGunNewPosition()
    {
        ChangeGunParent.Instance.SetGunNewPos();
    }
    public void NewParentForRotate()
    {
        transform.GetChild(1).transform.parent = gunPieceParent.transform;
        NewParentForRotateControl();
    }
    private void NewParentForRotateControl()
    {
        if(transform.childCount > 1)
        {
            NewParentForRotate();
        }
    }
    public void GunShake()
    {
        gunPieceParent.transform.DOLocalRotate(_gunShakePos, 0.13f);
        Invoke("GunShakePosGetBack", 0.15f);
    }
    private void GunShakePosGetBack()
    {
        gunPieceParent.transform.DOLocalRotate(Vector3.zero, 0.13f);
    }

}
