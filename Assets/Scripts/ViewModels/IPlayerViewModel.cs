using UnityEngine;

namespace ViewModels
{
    public interface IPlayerViewModel
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        bool IsRun { get; set; }
        bool IsThrow { get; set; }
        int DiskCount { get; set; }
        Transform NeckTransform { get; }
        Transform AverageHandsTransform { get; }
        Transform TubeRoundParent { get; }
    }
}