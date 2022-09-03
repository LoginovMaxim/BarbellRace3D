using UnityEngine;

namespace VisualEffects
{
    [RequireComponent(typeof(ParticleSystem))]
    public abstract class BaseEffect : MonoBehaviour, IEffect
    {
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        #region IEffect

        public abstract EffectId EffectId { get; }
        
        ParticleSystem IEffect.ParticleSystem => _particleSystem;

        #endregion
    }
}