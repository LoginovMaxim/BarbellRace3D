using UI;
using UnityEngine;
using UnityWeld.Binding;
using ViewModels;

namespace Views
{
    [Binding] public sealed class BarbellTrackRoadSegmentViewModel : ViewModel, IBarbellTrackRoadSegmentViewModel
    {
        [SerializeField] private MeshRenderer _gounrdMeshRenderer;
        
        public Transform Transform => transform;
        
        [Binding] public bool IsAchieved
        {
            get
            {
                return _isAchieved;
            }
            set
            {
                if (_isAchieved == value)
                {
                    return;
                }

                _isAchieved = value;
                OnPropertyChanged(nameof(IsAchieved));
            }
        }

        [Binding] public string DistanceText
        {
            get
            {
                return _distanceText;
            }
            set
            {
                if (_distanceText == value)
                {
                    return;
                }

                _distanceText = value;
                OnPropertyChanged(nameof(DistanceText));
            }
        }
        
        [Binding] public Color GroundColor
        {
            get
            {
                return _groundColor;
            }
            set
            {
                if (_groundColor == value)
                {
                    return;
                }

                _groundColor = value;
                _gounrdMeshRenderer.material.color = _groundColor;
                OnPropertyChanged(nameof(GroundColor));
            }
        }
        
        private bool _isAchieved;
        private string _distanceText;
        private Color _groundColor;
    }
}