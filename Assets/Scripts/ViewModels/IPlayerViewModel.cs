using System.Collections.Generic;
using UnityEngine;

namespace ViewModels
{
    public interface IPlayerViewModel
    {
        List<Vector3> MovementBuffer { get; set; }
        Transform Transform { get; }
        bool IsRun { get; set; }
        int LeftDiskCount { get; set; }
        int RightDiskCount { get; set; }
        Transform TubeRoundParent { get; }
    }
}