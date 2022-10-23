using System;
using Services;
using UnityEngine;

namespace Monos
{
    public interface IMonoUpdater
    {
        void Subscribe(UpdateType updateType, Action action);
        void Unsubscribe(UpdateType updateType, Action action);
    }
}