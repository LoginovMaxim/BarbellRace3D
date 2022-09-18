using System;

namespace App.Services
{
    [Flags] public enum UpdateType
    {
        Update = 1,
        FixedUpdate = 2,
        LateUpdate = 4
    }
}