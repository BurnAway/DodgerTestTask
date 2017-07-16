using System;
using System.Collections.Generic;
using UnityEngine;

class CameraController : MonoBehaviour
{
    private Location _location;
    private FocusArea _focusArea;
    private Transform _target;
    private float _borderMove;

    void Start()
    {
        _focusArea = CreateFocusArea();
    }

    public void Initialize(Location location, Transform target, CameraConfig config)
    {
        _location = location;
        _target = target;
        _borderMove = config.BorderMove;
    }

    void Update()
    {
        _focusArea.Update(_target.position);

        float posX = Mathf.Clamp(_focusArea.Center.x, _location.Rect.min.x + LevelConfig.WorldScreenWidth / 2,
            _location.Rect.max.x - LevelConfig.WorldScreenWidth / 2);

        float posY = Mathf.Clamp(_focusArea.Center.y, _location.Rect.min.y + LevelConfig.WorldScreenHeight / 2,
            _location.Rect.max.y - LevelConfig.WorldScreenHeight / 2);
        
        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    private FocusArea CreateFocusArea()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        return new FocusArea(new Vector2(cameraHeight * screenAspect * _borderMove, cameraHeight * _borderMove));
    }

    public class FocusArea
    {
        public Vector2 Center;
        private float _left;
        private float _right;
        private float _top;
        private float _bottom;

        public FocusArea(Vector2 size)
        {
            _left = -size.x / 2;
            _right = size.x / 2;
            _bottom = -size.y / 2;
            _top = size.y / 2;
            Center = new Vector2((_left + _right) / 2, (_top + _bottom) / 2);
        }

        public void Update(Vector2 targetPosition)
        {
            float shiftX = 0;

            if (targetPosition.x < _left)
            {
                shiftX = targetPosition.x - _left;
            }
            else if (targetPosition.x > _right)
            {
                shiftX = targetPosition.x - _right;
            }

            _left += shiftX;
            _right += shiftX;

            float shiftY = 0;
            if (targetPosition.y < _bottom)
            {
                shiftY = targetPosition.y - _bottom;
            }
            else if (targetPosition.y > _top)
            {
                shiftY = targetPosition.y - _top;
            }

            _top += shiftY;
            _bottom += shiftY;
            Center = new Vector2((_left + _right) / 2, (_top + _bottom) / 2);
        }
    }
}