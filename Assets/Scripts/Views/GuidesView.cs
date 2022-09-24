using UnityEngine;

namespace Views
{
    public sealed class GuidesView : MonoBehaviour
    {
        public Transform Handle => _handle;
        
        public float Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (_progress == value)
                {
                    return;
                }

                _progress = value;
                MoveHandle();
            }
        }
        
        public Vector3 HandleOffset
        {
            get
            {
                return _handleOffset;
            }
            set
            {
                if (_handleOffset == value)
                {
                    return;
                }

                _handleOffset = value;
            }
        }
        
        [SerializeField] private Transform _handle;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _finishPoint;

        private float _progress;
        private Vector3 _handleOffset;
        
        public void MoveHandle()
        {
            _handle.transform.position = Vector3.Lerp(_startPoint.position + _handleOffset, _finishPoint.position + _handleOffset, _progress);
        }
    }
}