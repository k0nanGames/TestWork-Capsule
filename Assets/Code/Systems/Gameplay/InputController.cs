using UnityEngine;
using TestWork.Core;
using System.Collections.Generic;
using System.Linq;

namespace TestWork.Input
{
    public class InputController : MonoBehaviour, IGameSystem
    {
        private Vector2 _movement = Vector2.zero;
        private Vector2 _rotation = Vector2.zero;

        private List<int> _touches = new List<int>();

        public void Init()
        {
            UnityEngine.Input.multiTouchEnabled = true;
        }

        public bool IsTouchAvaible(int fingerId)
        {
            return _touches.FindAll(x=> x == fingerId).Count == 0;
        }

        public List<int> GetTouches()
        {
            return _touches;
        }

        public void AddTouch(int fingerId)
        {
            if (!_touches.Contains(fingerId))
                _touches.Add(fingerId);
        }

        public void ClearTouches()
        {
            _touches.Clear();
        }

        public void RemoveTouch(int fingerId)
        {
            if(_touches.Contains(fingerId))
                _touches.Remove(fingerId);
        }

        /// <summary>
        /// Returns Movement or Rotation axis
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Vector2 GetAxis(InputStickType type)
        {
            switch (type)
            {
                case InputStickType.Walk:
                    return _movement;

                case InputStickType.Rotation:
                    return _rotation;

                default:
                    return Vector2.zero;
            }
        }

        /// <summary>
        /// Set Movement or Rotation axis value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="axis"></param>
        public void SetAxis(InputStickType type, Vector2 axis)
        {
            switch (type)
            {
                case InputStickType.Walk:
                    _movement = axis;
                    break;

                case InputStickType.Rotation:
                    _rotation = axis;
                    break;

                default:
                    break;
            }
        }
    }
}