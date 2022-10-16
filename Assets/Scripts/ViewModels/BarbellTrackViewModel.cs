using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding] public sealed class BarbellTrackViewModel : ViewModel, IBarbellTrackViewModel
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _finishPoint;
        
        private readonly List<IBarbellTrackRoadSegmentViewModel> _barbellTrackDistanceTextViewModels =
            new List<IBarbellTrackRoadSegmentViewModel>();

        private void Start()
        {
            var barbellTrackDistanceTextViewModels = GetComponentsInChildren<BarbellTrackRoadSegmentViewModel>();
            foreach (var barbellTrackDistanceTextViewModel in barbellTrackDistanceTextViewModels)
            {
                _barbellTrackDistanceTextViewModels.Add(barbellTrackDistanceTextViewModel);
            }

            var distance = 1.0f;
            foreach (var barbellTrackDistanceTextViewModel in _barbellTrackDistanceTextViewModels)
            {
                barbellTrackDistanceTextViewModel.DistanceText = $"x{distance:0.0}";
                distance += 0.2f;
            }
        }

        #region IBarbellTrackViewModel

        Transform IBarbellTrackViewModel.Transform => transform;
        Transform IBarbellTrackViewModel.StartPoint => _startPoint;
        Transform IBarbellTrackViewModel.FinishPoint => _finishPoint;

        List<IBarbellTrackRoadSegmentViewModel> IBarbellTrackViewModel.BarbellTrackDistanceTextViewModels =>
            _barbellTrackDistanceTextViewModels;

        void IBarbellTrackViewModel.SetFinishPosition(Vector3 finishPosition)
        {
            _finishPoint.position = finishPosition;
        }

        #endregion
    }
}