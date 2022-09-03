using UnityEngine;

namespace VisualEffects
{
    public interface IEffect
    {
        EffectId EffectId { get; }
        ParticleSystem ParticleSystem { get; }
    }
}