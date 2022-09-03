using System;
using System.Collections.Generic;
using UnityEngine;

namespace VisualEffects
{
    public sealed class EffectEmitter : IEffectEmitter
    {
        private readonly List<IEffect> _effects;

        public EffectEmitter(List<IEffect> effects)
        {
            _effects = effects;
        }

        private void Emit(EffectId effectId, Vector3 position, Color color)
        {
            var effect = GetEffectById(effectId);
            if (effect == null)
            {
                throw new Exception($"ParticleSystem for {effectId.ToString()} effect was not found");
            }

            var emitParams = new ParticleSystem.EmitParams
            {
                position = position,
                startColor = color,
                applyShapeToPosition = true
            };
            
            for (var i = 0; i < 20; i++)
            {
                effect.Emit(emitParams, 1);
            }
        }
        
        private ParticleSystem GetEffectById(EffectId effectId)
        {
            foreach (var effect in _effects)
            {
                if (effect.EffectId != effectId)
                {
                    continue;
                }

                return effect.ParticleSystem;
            }

            return null;
        }
        
        void IEffectEmitter.Emit(EffectId effectId, Vector3 position, Color color)
        {
            Emit(effectId, position, color);
        }
    }
}