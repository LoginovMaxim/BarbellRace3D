using System;
using UnityEngine;

namespace Views
{
    [Serializable] public struct TargetIKData
    {
        public Transform Self;
        public Transform Target;

        public void Align()
        {
            Self.position = Target.position;
            Self.rotation = Target.rotation;
        }
    }
}