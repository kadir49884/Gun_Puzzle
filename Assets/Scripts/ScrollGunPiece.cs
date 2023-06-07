using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGunPiece : MonoBehaviour
{

    private Vector2 _swipeDelta, _startTouch;
    private const float deadZone = 150;

    BaseControl baseControl;
    GameManager gameManager;

    private bool _slideStatus = true;
    private bool _magazineStatus = true;

    private float xAxis;
    private float yAxis;

    private void Start()
    {
        baseControl = BaseControl.Instance;
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        Player_move();
    }
    public void Player_move()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _startTouch = _swipeDelta = Vector2.zero;
        }


#elif UNITY_ANDROID

        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                _startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                _startTouch = _swipeDelta = Vector2.zero;
            }
        }
#endif
        _swipeDelta = Vector2.zero;
        if (_startTouch != Vector2.zero)
        {
            if (Input.touches.Length != 0)
            {
                _swipeDelta = Input.touches[0].position - _startTouch;
            }

            else if (Input.GetMouseButton(0))
            {
                _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
            }
        }

        IsHappenSlider();
    }

    private void IsHappenSlider()
    {
        if (_swipeDelta.magnitude > deadZone)
        {
            xAxis = _swipeDelta.x;
            yAxis = _swipeDelta.y;

            if (xAxis * yAxis < 0 && xAxis > 0 && gameManager.CurrentState == State.Slide && _slideStatus)
            {
                baseControl.SlideGoInBase();
                _slideStatus = false;
            }
            else if (yAxis > 0 && gameManager.CurrentState == State.Base && _magazineStatus)
            {
                baseControl.MagazineGoBase();
                _magazineStatus = false;
            }
            _startTouch = _swipeDelta = Vector2.zero;
        }
    }
}



