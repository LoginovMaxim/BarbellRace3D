using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Monos
{
    public sealed class InputReceiver : MonoBehaviour, IPointerDownHandler, IMoveHandler, IPointerUpHandler
    {
        public float Horizontal => _horizontal;

        public float Scale;
        public float Smooth;
        
        private Vector2 _previousMousePosition;
        private Vector2 _currentMousePosition;
        private float _horizontal;
        
        private void OnPointerDown(PointerEventData eventData)
        {
        }

        private void Update()
        {
            if (!Input.GetMouseButton(0))
            {
                return;
            }

            _currentMousePosition = Input.mousePosition;
            
            if (Input.GetMouseButtonDown(0))
            {
                _previousMousePosition = _currentMousePosition;
            }

            var delta = (_currentMousePosition - _previousMousePosition) / Scale;
            if (delta.x > 0)
            {
                _horizontal = Mathf.Min(delta.x, 1);
            }
            else if (delta.x < 0)
            {
                _horizontal = Mathf.Max(delta.x, -1);  
            }
            else if (delta.x == 0)
            {
                _horizontal = 0;
            }
            
            _previousMousePosition = Vector3.Lerp(_previousMousePosition, _currentMousePosition, Smooth);
        }

        private void OnPointerUp(PointerEventData eventData)
        {
        }

        #region IPointer

        
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnPointerDown(eventData);
        }

        void IMoveHandler.OnMove(AxisEventData eventData)
        {
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnPointerUp(eventData);
        }

        #endregion
    }
}