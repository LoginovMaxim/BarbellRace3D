using UnityEngine;

namespace ViewModels
{
    public interface IPlayerViewModel
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        bool IsRun { get; set; }
        int LeftDiskCount { get; set; }
        int RightDiskCount { get; set; }
        bool IsGrounded { get; set; }
        Transform TubeRoundParent { get; }
    }
}