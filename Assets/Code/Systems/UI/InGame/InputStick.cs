using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TestWork.Core;

namespace TestWork.Input
{
    public class InputStick : MonoBehaviour
    {
        [SerializeField] private InputStickType _stickType;
        [SerializeField] private Image _inputZone;
        [SerializeField] private Image _inputPointer;
        [SerializeField] private Image _inputStick;

        private InputController _inputController;
        private Vector2 _startPos = Vector2.zero;
        
        private int _currentFingerId = -1;

        private void Awake()
        {
            _inputController = CoreGameManager.GetInstance<InputController>();
        }

        public void Update()
        {
            if(UnityEngine.Input.touchCount > 0)
            {
                if (_startPos == Vector2.zero)
                {
                    FindAvaibleTouch();
                }

                if (_currentFingerId != -1)
                {
                    bool isTouchFinded = false;
                    foreach (Touch touch in UnityEngine.Input.touches)
                    {
                        if (touch.fingerId == _currentFingerId)
                        {
                            isTouchFinded = true;

                            if (RectTransformUtility.RectangleContainsScreenPoint(_inputZone.rectTransform, touch.position) || _startPos != Vector2.zero)
                            {
                                UpdateInput(touch.position);
                            }
                            else
                            {
                                ResetInput();
                            }

                            break;
                        }
                    }

                    if (!isTouchFinded)
                    {
                        ResetInput();
                    }
                }
            }
            else
            {
                ResetInput();
            }
        }

        private void FindAvaibleTouch()
        {
            foreach (Touch touch in UnityEngine.Input.touches)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(_inputZone.rectTransform, touch.position))
                {
                    if (_inputController.IsTouchAvaible(touch.fingerId))
                    {
                        _currentFingerId = touch.fingerId;
                        _startPos = touch.position;
                        _inputController.AddTouch(touch.fingerId);
                        _inputPointer.rectTransform.position = _startPos;

                        break;
                    }
                }
            }
        }

        private void UpdateInput(Vector2 currentPoint)
        {
            Vector2 axis = Vector2.ClampMagnitude(_startPos - currentPoint, _inputPointer.rectTransform.rect.width / 2f);

            _inputStick.rectTransform.localPosition = axis * -1;

            _inputController.SetAxis(_stickType, (1f / (_inputPointer.rectTransform.rect.width / 2f)) * axis);
        }

        private void ResetInput()
        {
            _startPos = Vector2.zero;
            _inputStick.rectTransform.localPosition = Vector3.zero;

            if (!_inputController.IsTouchAvaible(_currentFingerId))
                _inputController.RemoveTouch(_currentFingerId);

            _currentFingerId = -1;

            _inputController.SetAxis(_stickType, Vector2.zero);
        }
    }
}