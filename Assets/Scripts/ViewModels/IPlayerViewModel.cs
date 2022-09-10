﻿using UnityEngine;

namespace ViewModels
{
    public interface IPlayerViewModel
    {
        Transform Transform { get; }
        bool IsRun { get; set; }
        int LeftDiskCount { get; set; }
        int RightDiskCount { get; set; }
    }
}