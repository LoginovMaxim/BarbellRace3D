using UnityEngine;

namespace ViewModels
{
    public interface IPlayerViewModel
    {
        Transform Transform { get; }
        Rigidbody Rigidbody { get; }
        bool IsRun { get; set; }
        int DiskCount { get; set; }
        bool IsGrounded { get; set; }
        Transform BarbellParent { get; }
        Transform TubeRoundParent { get; }
    }
}