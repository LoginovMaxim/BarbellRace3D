using UnityEngine;

namespace ViewModels
{
    public interface IBarbellTrackRoadSegmentViewModel
    {
        Transform Transform { get; }
        bool IsAchieved { get; }
        string DistanceText { get; set; }
        Color GroundColor { get; set; }
    }
}