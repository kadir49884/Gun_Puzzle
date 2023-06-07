using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TouchControl : MonoBehaviour
{

    private Vector3 _lastTouchPos;
    private float _dragSpeed = 10f;
    private bool _isMouseUp = false;

    GameManager gameManager;
    BaseControl baseControl;

    private GameObject _bulletTransient;

    private Rigidbody rb;

    private Camera camera;


    ObjectManager objectManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        baseControl = BaseControl.Instance;
        objectManager = ObjectManager.Instance;
        rb = GetComponent<Rigidbody>();
        camera = objectManager.CameraObject;
    }
    void Update()
    {
        if (gameManager.CurrentState == State.Bullet)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastTouchPos = camera.ScreenToWorldPoint(Input.mousePosition);
                rb.useGravity = false;
                _isMouseUp = true;
            }
        }
    }
    private void OnMouseDrag()
    {
        if (gameManager.CurrentState == State.Bullet)
        {
            Vector3 screenToWorld = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = screenToWorld - _lastTouchPos;
            Vector3 pos = transform.position;
            pos = new Vector3(pos.x += delta.x * _dragSpeed, pos.y = 2.5f, pos.z += delta.y * _dragSpeed);

            transform.position = Vector3.Lerp(transform.position, pos, 0.09f);
            _lastTouchPos = screenToWorld;
        }
    }
    private void OnMouseUp()
    {
        _lastTouchPos = camera.ScreenToWorldPoint(Input.mousePosition);
        rb.useGravity = true;
        _isMouseUp = false;
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Statics.GUN_MAGE) && gameManager.CurrentState == State.Bullet)
        {
            if (!_isMouseUp)
            {
                gameObject.tag = Statics.GUN_BULLETINMAGE;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                transform.DOLocalMove(new Vector3(0, 0.05f, 0), 0.4f);
                Invoke("GoBulletMagePosLate", 0.3f);
                Invoke("LateSetChild", 0.4f);
            }
        }
    }
    private void GoBulletMagePosLate()
    {
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    private void LateSetChild()
    {
        if (transform.parent.childCount < 2)
        {
            Invoke("MagazineWaitForStateUp", 2f);
            baseControl.RotateAndTransformBase();
        }
        _bulletTransient = transform.parent.transform.parent.GetChild(0).transform.gameObject;
        _bulletTransient.transform.DOLocalMoveY(_bulletTransient.transform.localPosition.y - 0.04f, 0.3f);
        Invoke("LateSetChildAgain", 0.5f);
    }
    private void LateSetChildAgain()
    {
        gameObject.transform.parent = _bulletTransient.transform;
    }
    private void MagazineWaitForStateUp()
    {
        gameManager.CurrentState++;
    }

}
