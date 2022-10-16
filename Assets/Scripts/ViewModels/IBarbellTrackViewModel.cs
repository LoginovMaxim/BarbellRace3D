using System.Collections.Generic;
using UnityEngine;

namespace ViewModels
{
    public interface IBarbellTrackViewModel
    {
        Transform Transform { get; }
        Transform StartPoint { get; }
        Transform FinishPoint { get; }
        List<IBarbellTrackRoadSegmentViewModel> BarbellTrackDistanceTextViewModels { get; }
        void SetFinishPosition(Vector3 finishPosition);
    }
}