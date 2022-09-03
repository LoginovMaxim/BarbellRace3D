using UnityEngine;

namespace VisualEffects
{
    public interface IEffectEmitter
    {
        void Emit(EffectId effectId, Vector3 position, Color color);
    }
}