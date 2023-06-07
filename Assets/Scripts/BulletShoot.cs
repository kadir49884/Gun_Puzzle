using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletShoot : MonoBehaviour
{

    [SerializeField] Settings settings;
    private static BulletShoot instance;
    public static BulletShoot Instance { get => instance; set => instance = value; }


    private Rigidbody rigidbody;

    private TargetControl targetControl;

    private BaseControl baseControl;

    private void Awake()
    {
        transform.parent = null;
    }
    private void Start()
    {
        baseControl = BaseControl.Instance;
        rigidbody = GetComponent<Rigidbody>();
        Invoke("LateGunShake", 0.1f);
        Invoke("DestroyBullet", 3);
        targetControl = TargetControl.Instance;
        rigidbody.AddForce(transform.forward * settings.BulletSpeed);
    }

    private void LateGunShake()
    {
        baseControl.GunShake();
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject _gameObject = other.gameObject;
        if (_gameObject.CompareTag(Statics.TARGET_TARGETOBJECT))
        {
            _gameObject.GetComponent<BoxCollider>().enabled = false;
            _gameObject.transform.DOLocalRotate(new Vector3(90, 0, 0), 1.5f, RotateMode.Fast);
            targetControl.ReduceTarget();
            Destroy(gameObject);
        }
    }
}
